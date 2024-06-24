using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsMainTypes.SubType;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsWARSTAT.Services;
using GemsOfWarsWARSTAT.TextAnalysis;
using SimpleControlLibrary.Tree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static GemsOfWarsMainTypes.GlobalConstants;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public partial class WarDayImportViewModel : WarsDayAddViewModel
    {
        private void OnTryReadFromImage()
        {
            User user = null;
            Guild guild = null;
            DateTime date;
            ColorUnits colorDay = ColorUnits.None;
            War war = null;
            WarDayReader warDayReader = null;
            WarDay lastWarDay = null;
            try
            {
               
                string directoryNick;

                user = FindUser(out directoryNick);

                guild = FindGuid(directoryNick, out var directoryGuild);

                date = FindDateWar(directoryGuild);
              

                var readWar = Context.Wars.Include(x=>x.MapColor).Where(x => x.DateStart == date).FirstOrDefault();
                if (readWar == null)
                {
                    war = new War();
                    war.DateStart = date;
                  
                    var message = Messages.AddWar.Args(war.DisplayName);
                    var result = MessageBox.Show(message, "Добавить войну", MessageBoxButton.OKCancel);
                    if (result != MessageBoxResult.OK)
                    {
                        return;
                    }
                    //SelectColorMap(war);
                    var colorMap = Context.WarColorMaps.First();
                    war.MapColor = colorMap;
                    Context.Wars.Add(war);
                    Wars.Add(war);
                }
                else
                {
                   war = readWar;
                }

                var colormap = readWar.MapColor;
                var dict = colormap.GetDayNumber();
                colorDay = GetColorDay(dict);

                var lastUserWars = Context.Wars.Where(x => x.DateStart < date).OrderByDescending(x => x.DateStart).ToList();

                lastWarDay = FindLatestWarDay(lastUserWars, colorDay, user);
            }
            catch (Exception)
            {
                var message = "При чтении параметров была ошибка!";
                var result = MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            try
            {
                warDayReader = new WarDayReader(Context, SelectImageFile);
                warDayReader.StartRead(1);
            }
            catch (Exception)
            {
                var message = "При чтении из файла была ошибка!";
                var result = MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                warDayReader = null;

            }
            var select = new WarDay();
            select.User = user;
            select.ColorDay = colorDay;
            select.War = war;
            select.Defence = new Defence();
            if (lastWarDay != null)
            {
                select.Defence.ReadFromItem(lastWarDay.Defence);
            }
            if (warDayReader != null)
            {
                select.Victories = warDayReader.WR.Item1;
                select.Losses = warDayReader.WR.Item2;
            }

            if (war != null && guild != null && user != null)
            {
                var inGuilds = Context.UsersInGuilds.Where(x=> x.User.Id == user.Id).ToArray();
                if (!inGuilds.Any(x=>x.InWar(war)))
                {
                    var message = "Пользователь не находится в гильдии, в указанной директории! Добавить?";
                    var result = MessageBox.Show(message, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        var userInGuild = new UserInGuild();
                        userInGuild.User = user;
                        userInGuild.Guild = guild;
                        userInGuild.DateStart = war.DateStart;
                        Context.UsersInGuilds.Add(userInGuild);
                    }

                }
            }
            

            Selection = select;
            IsEditing = true;
        }

        //private void SelectColorMap(War war)
        //{
        //    throw new NotImplementedException();
        //}

        private WarDay FindLatestWarDay(List<War> lastUserWars, ColorUnits colorDay, User user)
        {
            foreach (var war in lastUserWars)
            {
                var warday = Context.WarDays.FirstOrDefault(
                    x => x.ColorDay == colorDay
                        && x.User.Id == user.Id
                        && x.War.Id == war.Id);
                if (warday != null)
                {
                    return warday;
                }
            }
            return null;
        }

        private ColorUnits GetColorDay(Dictionary<int, ColorUnits> dayNumber)
        {
            var name = Path.GetFileNameWithoutExtension(SelectImageFile);
            if (int.TryParse(name, out var color))
            {
                return dayNumber[color];
            }
            return dayNumber[1];
        }

        private static DateTime FindDateWar(string directoryGuild)
        {
            var dateDirectory = Path.GetDirectoryName(directoryGuild);
            var dateStr = Path.GetFileName(dateDirectory);

            var yearDirectory = Path.GetDirectoryName(dateDirectory);
            var year = Path.GetFileName(yearDirectory);

            var dateOnly = (dateStr + "." + year).Split().Last();

            var date = DateTime.ParseExact(dateOnly, "MM.dd.yyyy", null);
            return date;
        }

        private Guild FindGuid(string directoryNick, out string directoryGuild)
        {
            directoryGuild = Path.GetDirectoryName(directoryNick);
            var guildnick = Path.GetFileName(directoryGuild);

            var guild = CompareTextHelper.FindBestItem(guildnick, Guilds, x => x.Name);
            return guild;
        }

        private User FindUser(out string directoryNick)
        {
            var dn = Path.GetDirectoryName(SelectImageFile);
            directoryNick = dn;
            var findUser = Context.FileLocations.OfType<UserFolderLocation>().FirstOrDefault(x=>x.Path == dn);  
            if (findUser == null)
            {
                var nick = Path.GetFileName(directoryNick);
                var user = CompareTextHelper.FindBestItem(nick, Users, x => x.Name);
                return user;
            }
            return findUser.User;
        }
    }
}
