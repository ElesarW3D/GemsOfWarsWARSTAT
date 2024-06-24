using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GemsOfWarsWARSTAT.Services;
using System.Data.Entity;
using System.Linq;
using GemsOfWarsMainTypes.Extension;
using System.Collections.Generic;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsMainTypes.SubType;

namespace DataBaseTest
{
    [TestClass]
    public class TestTables
    {
        private Dictionary<string, int> _exceptions = new Dictionary<string, int>
        {
            {"Dipkyn",2 }  // 2 - 18.07.2022
        };

        [TestMethod]
        public void TestWarDay6AndAnyColor()
        {
            WarDbContextService warDbContextService = new WarDbContextService();
            var context = warDbContextService.Context;
            context.HeroClasses.Load();
            context.Units.Load();
            var units = context.Units.Local.ToList();

            var wars = context.Wars.OrderBy(x => x.DateStart).ToObservableCollection();

          
            context.WarDays.Include(x=>x.User).Include(x=>x.War).Load();
            var warDays = context.WarDays.Local
                .OrderBy(x => x.War.DateStart)
                .ThenBy(x => x.User.Name)
                .ThenBy(x => x.ColorDay)
                .ToObservableCollection();
            
            context.HeroClasses.Load();
            context.Defences.Load();
            context.Defences
               .Include(db => db.Units1)
               .Include(db => db.Units2)
               .Include(db => db.Units3)
               .Include(db => db.Units4)
               .Load();
            var defences = context.Defences.Local.ToArray()
               .ToObservableCollection();

            var warsGroup = warDays.GroupBy(x => x.War);
            var allCheck = true;
            foreach (var warGroup in warsGroup)
            {
                var war = warGroup.Key;
                var userCheck = true;
                var warDayUsers = warGroup.GroupBy(x => x.User);
                foreach (var warDayUser in warDayUsers)
                {
                    var user = warDayUser.Key;
                    if (_exceptions.ContainsKey(user.Name) && _exceptions[user.Name] == war.Id)
                    {
                        continue;
                    }

                    var hasSix = warDayUser.Count() == 6;
                    if (!hasSix)
                    {
                        userCheck = false;
                        Console.WriteLine($"Users {user.Name} dont have 6 colors in war:{war.DisplayName}");
                    }
                    var colors = ColorUnits.All.GetByOneColor();
                    foreach (var color in colors)
                    {
                        var dayColors = warDayUser.Where(x=>x.ColorDay == color);
                        var countDayColors = dayColors.Count();
                        if (countDayColors > 1)
                        {
                            Console.WriteLine($"Users {user.Name} have extra color on  {color} warDay in war:{war.DisplayName}");
                        }
                        var dayColor = dayColors.FirstOrDefault();
                        if (dayColor == null)
                        {
                            userCheck = false;
                            Console.WriteLine($"Users {user.Name} dont have on {color} warDay in war:{war.DisplayName}");
                        }
                    }

                    allCheck &= userCheck;
                }

            }
            Assert.IsTrue(allCheck);
        }
            
        [TestMethod]
        public void TestNameUnit()
        {
            WarDbContextService warDbContextService = new WarDbContextService();
            var context = warDbContextService.Context;
            context.HeroClasses.Load();
            context.Units.Load();
            var units = context.Units.Local.ToList();

            var unicName = new Dictionary<string, List<int>>();
            CreateUnicCollection(units, unicName);
            var notUnicName = unicName.Where(x => x.Value.Count > 1).ToList();
            bool isFind = false;
            foreach (var item in notUnicName)
            {
                isFind = true;
                var ids = string.Join(",", item.Value);
                Console.WriteLine($"for {item.Key} has dublicates {ids}");
            }

            Assert.IsFalse(isFind);
        }

        [TestMethod]
        public void TestDefenceDublicates()
        {
            WarDbContextService warDbContextService = new WarDbContextService();
            var context = warDbContextService.Context;
            context.HeroClasses.Load();
            context.Units.Load();
            context.Defences.Load();
            context.Defences
                .Include(db => db.Units1)
                .Include(db => db.Units2)
                .Include(db => db.Units3)
                .Include(db => db.Units4)
                .Load();

            var defences = context.Defences.Local.ToArray()
                .OrderByUnits()
                .ToObservableCollection();

            var testDb = new Dictionary<string, int>();
            var isCorrect = true;
            foreach (var defence in defences)
            {
                var value = defence.Id;
                var key = defence.DisplayName;
                if (testDb.ContainsKey(key))
                {
                    var oldvalue = testDb[key];
                    isCorrect = false;
                    Console.WriteLine($"Defence duplicate found with id {value} and oldid{oldvalue}");
                }
                else
                {
                    testDb.Add(key, value);
                }
            }
            Assert.IsTrue(isCorrect);
        }


        private static void AddToFind(HashSet<Tuple<Defence, Defence>> dontCorrectDef, IEnumerable<Defence> findDef, IEnumerable<Defence> findDef1)
        {
            if (findDef.Any())
            {
                var fdOrigin = findDef.ToList();
                var fdDubl = findDef1.ToList();

                for (int i = 0; i < fdOrigin.Count; i++)
                {
                    var addTuple = new Tuple<Defence, Defence>(fdOrigin[i], fdDubl[i]);
                    dontCorrectDef.Add(addTuple);
                }
            }
        }

        private static void CreateUnicCollection(List<Unit> units, Dictionary<string, List<int>> unicName)
        {
            foreach (var item in units)
            {
                var key = item.DisplayName;
                var value = item.Id;
                if (unicName.ContainsKey(key))
                {
                    unicName[key].Add(value);
                }
                else
                {
                    var list = new List<int>();
                    list.Add(value);
                    unicName.Add(key, list);
                }
            }
        }
    }
}
