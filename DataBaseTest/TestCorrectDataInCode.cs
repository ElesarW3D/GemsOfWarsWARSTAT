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
    public class TestCorrectDataInCode
    {
        [TestMethod]
        public void TestCorrectData()
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

            var codeInDef = new Dictionary<string, int>();
            var correct = true;
            foreach (var item in defences)
            {
                var code = item.Code;
                var checkItem = true;
                if (!GetCodeItems(code, out var codes))
                {
                    continue;
                }
                checkItem &= CheckItem(0, codes, item.Units1, codeInDef);
                checkItem &= CheckItem(1, codes, item.Units2, codeInDef);
                checkItem &= CheckItem(2, codes, item.Units3, codeInDef);
                checkItem &= CheckItem(3, codes, item.Units4, codeInDef);
                if (!checkItem)
                {
                    Console.WriteLine($"Защита {item.DisplayName} c id {item.Id} не прошла проверку");
                }
                correct &= checkItem;
            }
            Assert.IsTrue(correct);

            //foreach (var code in codeInDef)
            //{
            //    var units = context.Units.Where(x => x.Name == code.Key);
            //    foreach (var unit in units)
            //    {
            //        Assert.IsTrue(unit != null);
            //        unit.GameId = code.Value;
            //    }
            //}
            //context.SaveChanges();
        }

        [TestMethod]
        public void UpdateCode()
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
            bool isGenerate = false;
            foreach (var defence in defences)
            {
                if (!string.IsNullOrEmpty( defence.Code))
                {
                    continue;
                }
                var code = string.Empty;
                var classCode = string.Empty;
                code = GenerateByUnit(code, defence.Units1, ref classCode);
                code = GenerateByUnit(code, defence.Units2, ref classCode);
                code = GenerateByUnit(code, defence.Units3, ref classCode);
                code = GenerateByUnit(code, defence.Units4, ref classCode);
                var number = GenerateNumber(defence.Units1.ColorUnits, defence.Units2.ColorUnits, defence.Units3.ColorUnits, defence.Units4.ColorUnits);
                code = "[" + code + $"{number}," + classCode + "]";
                defence.Code = code;
                isGenerate = true;
                Console.WriteLine($"For def {defence.DisplayName} generate code {defence.Code}");
            }
            if (isGenerate)
            {
                context.SaveChanges();
            }
           
            Assert.IsFalse(isGenerate);
        }

        private string GenerateNumber(ColorUnits colorUnits1, ColorUnits colorUnits2, ColorUnits colorUnits3, ColorUnits colorUnits4)
        {
            Dictionary<ColorUnits, int> colorsMap = new Dictionary<ColorUnits, int>();
            foreach (var color in ColorUnits.All.GetByOneColor())
            {
                AddColorToMap(colorsMap, color);
            }
            foreach (var color1 in colorUnits1.GetByOneColor())
            {
                AddColorToMap(colorsMap, color1);
            }
            foreach (var color2 in colorUnits2.GetByOneColor())
            {
                AddColorToMap(colorsMap, color2);
            }
            foreach (var color3 in colorUnits3.GetByOneColor())
            {
                AddColorToMap(colorsMap, color3);
            }
            foreach (var color4 in colorUnits4.GetByOneColor())
            {
                AddColorToMap(colorsMap, color4);
            }


            WarDbContextService warDbContextService = new WarDbContextService();
            var context = warDbContextService.Context;
            context.Banners
                .Include(db => db.Colors)
                .Include(db => db.BannerName)
                .Load();
            var banners = context.Banners.Local.ToArray();
            var realBanner = banners.GroupBy(db => db.BannerName);
            var SumColorMulBanner = new Dictionary<BannerName, int>();
            foreach (var item in realBanner)
            {
                var sum = 0;
                foreach(var banner in item)
                {
                    var colorCount = banner.Colors;
                    var points = colorsMap[colorCount.Color];
                    sum += points * colorCount.Count;
                }
                SumColorMulBanner.Add(item.Key, sum);
            }
            var max = SumColorMulBanner.Max(x => x.Value);
            var BannerId = SumColorMulBanner.Where(x => x.Value == max).FirstOrDefault().Key.GameId.ToString();
            return BannerId;
        }

        private static void AddColorToMap(Dictionary<ColorUnits, int> countColors, ColorUnits color1)
        {
            if (countColors.ContainsKey(color1))
            {
                countColors[color1]++;
            }
            else
            {
                countColors[color1] = 0;
            }
        }

        private string GenerateByUnit(string code, Unit unit, ref string classCode)
        {
            if (unit is HeroWeapon heroWeapon)
            {
                var hero = heroWeapon.HeroClass;
                classCode = hero.GameTrait + "," + hero.GameId;
            }
            code = code + unit.GameId + ",";
            return code;
        }

        [TestMethod]
        public void TestAllAddUnit()
        {
            WarDbContextService warDbContextService = new WarDbContextService();
            var context = warDbContextService.Context;
            context.HeroClasses.Load();
            context.Units.Load();
            var units = context.Units.ToList();
            var correct = true;
            foreach (var unit in units)
            {
                if (unit.GameId == 0)
                {
                    correct &= false;
                    Console.WriteLine($"Unit id {unit.Id} имя {unit.DisplayName} не имеет кода");
                }
            }
            Assert.IsTrue(correct);
        }


        private bool CheckItem(int index, int[] codes, Unit units, Dictionary<string, int> codeInDef)
        {
            var code = codes[index];
            var name = units.Name;
            if (codeInDef.ContainsKey(name))
            {
                var oldCode = codeInDef[name];
                if (oldCode != code)
                {
                    Console.WriteLine($"Имя {name} уже есть в базе с кодом {oldCode} и пытается записаться как {code}");
                    return false;
                }
            }
            else
            {
                codeInDef[name] = code;
            }
            return true;
        }

        private bool GetCodeItems(string code, out int[] codes)
        {
            codes = new int[] { 0, 0, 0, 0 }; 
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            var start = code.IndexOf("[");
            var end = code.IndexOf("]");
            if (start == -1 || end == -1)
            {
               return false;
            }
            var numbers = code.Substring(start+1, end - start-1);
            codes = numbers.Split(',').Select(i => int.Parse(i)).Take(4).ToArray();
            return true;
        }
    }
}
