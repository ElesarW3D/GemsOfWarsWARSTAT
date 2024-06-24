using GemsOfWarsMainTypes.Model;
using GemsOfWarsWARSTAT.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace GemsOfWarsWARSTAT.TextAnalysis
{
    public class WarDayReader
    {
        public WarDayReader(WarDbContext warDbContext, string pathImage)
        {
            WarDbContext = warDbContext;
            PathImage = pathImage;
        }

        public WarDbContext WarDbContext { get; private set; }
        public string PathImage { get; private set; }
        public Defence Defence { get; private set; }
        public Tuple<int, int> WR { get; private set; }
        
        public void StartRead(int attempts = 3)
        {
            var defences = new List<Defence>(attempts);
            var wrs = new List<Tuple<int, int>>();
            LoadUnits();
            var percent = 75;
            var dPercent = 1;
            for (int i = 0; i < attempts; i++)
            {
                var texts = ReadTextsFromImage(PathImage, percent);
                percent += dPercent;
                var allText = string.Join("", texts);

                var wr = FindDefence(allText);
                wrs.Add(wr);
            }

            FindAndSaveBestWr(wrs);
        }

        private void FindAndSaveBestWr(List<Tuple<int, int>> wrs)
        {
            var wins = wrs.Select(x => x.Item1).ToList();
            var loss = wrs.Select(x => x.Item2).ToList();
            int bestWin = wins
                .GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .First();
            int bestLos = loss
                .GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .First();
            WR = Tuple.Create(bestWin, bestLos);
        }

       

        private Dictionary<string, Unit> ReadUnits(string[] strings)
        {
            var dictionary = new Dictionary<string, Unit>();
            foreach (var item in strings)
            {

                var chekItem = item.Trim();
                if (chekItem.Length <= 2)
                {
                    continue;
                }
                var bestUnit = CompareTextHelper.FindBestItem(chekItem, Units, x => x.Name);
                dictionary.Add(chekItem, bestUnit);
            }
            return dictionary;
        }

        private List<string> ReadTextsFromImage(string fileName, int percentPixelThreshold)
        {
            var result = new List<string>();
            try
            {
                const string outText = "out.txt";
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                string currentPath = GetParentDir();
                var readerExe = @"\ReadText\ReadText\bin\Debug\net6.0-windows\ReadText.exe";
                var fullPath = currentPath + readerExe;
                var fullOutPath = Path.Combine(currentPath, outText);
                p.StartInfo.FileName = fullPath;
                p.StartInfo.Arguments = "\"" + fileName + "\" \"" + fullOutPath + "\" \"" + percentPixelThreshold.ToString() + "\"";
                p.Start();
                p.WaitForExit();

                result = File.ReadAllLines(fullOutPath).ToList();
                File.Delete(fullOutPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private void LoadUnits()
        {
            WarDbContext.Units.Load();
            Units = WarDbContext.Units.ToList();
        }

        List<Unit> Units = new List<Unit>();

        private string RuFilter(string text)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                var letter = text[i];
                if (CompareTextHelper.IsRussian(letter)
                    || char.IsControl(letter)
                    || char.IsWhiteSpace(letter)
                    || letter == '-')
                {
                    sb.Append(letter);
                }
            }
            return sb.ToString();
        }

       

        public static Tuple<int, int> FindDefence(string text)
        {
            var win = 0;
            var los = 0;
            var pattern = @"З[ао][шщ]ита: (\d+)[/]?(\d*)";
            var matches = Regex.Matches(text, pattern);
            if (matches.Count == 0)
            {
                return Tuple.Create(win, los);
            }
            var match = matches[0];
          
            if (match.Groups[2].Length == 0)
            {
                var digit = match.Groups[1].Value;
                if (digit[0] == '0' && digit.Length>2)
                {
                    win = 0;
                    if (int.TryParse(digit.Substring(2,digit.Length - 2), out los))
                    {
                        return Tuple.Create(win, los);
                    }
                }
                var index7 = digit.IndexOf('7');
                if (index7 > 0)
                {
                    var winstr = digit.Substring(0,index7);
                    var strRest = digit.Length - 1 - index7;
                    var losstr = digit.Substring(index7 + 1, strRest);
                    int.TryParse(winstr, out win);
                    int.TryParse(losstr, out los);
                    return Tuple.Create(win, los);
                }
            }
            else
            {
                var winstr = match.Groups[1].Value;
                var losstr = match.Groups[2].Value;
                int.TryParse(winstr, out win);
                int.TryParse(losstr, out los);
            }
            return Tuple.Create(win, los);
        }

        
        private static string GetParentDir()
        {
            var current = Assembly.GetExecutingAssembly().Location;
            var parent1 = Directory.GetParent(current).FullName;
            var parent2 = Directory.GetParent(parent1).FullName;
            var parent3 = Directory.GetParent(parent2).FullName;
            var parent4 = Directory.GetParent(parent3).FullName;
            return parent4;
        }
    }
}
