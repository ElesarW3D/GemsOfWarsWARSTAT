using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarAnalitics;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsMainTypes.SubType;
using GemsOfWarsWARSTAT.DataContext;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GemsOfWarAnalitics.MainStats;
using GemsOfWarAnalitics.Filter;
using GemsOfWarsWARSTAT.Services;
using System.Diagnostics;

namespace GemsOfWarsWARSTAT.ViewModel.Statistic
{
    public partial class StatisticMainViewModel : BaseVisualViewModel
    {
        private void CalcUserUseStatTable()
        {
            CalculationStatistic.Clear();
            var warsSelect = WarCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem).ToList();
            if (warsSelect.Count>1)
            {
                MessageBox.Show("Работает только с 1 войной!");
                return;
            }
            var warSelect = warsSelect.FirstOrDefault();
            if (warSelect == null)
            {
                MessageBox.Show("Not war selected!");
                return;
            }
            var basketSelect = BasketCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem).ToList();
            if (warSelect == null)
            {
                MessageBox.Show("Not baskets selected!");
                return;
            }
            var calcUserTable = CalcDefencePercent();

            var calculateDictionary = new Dictionary<User, List<double>>();
            var totalsCalc = new List<TableUserCalc>();
            foreach (var user in _users)
            {
                var list = new List<double>(6);
                calculateDictionary.Add(user, list);
                var usersInGuilds = _userInGuild.Where(x => x.InWar(warSelect) && x.User == user);
                var guilds = usersInGuilds.Select(x => x.Guild).Distinct().ToList();
                if (guilds.Count == 0)
                {
                    totalsCalc.Add(new TableUserCalc(user, GetEmptyList()));
                    continue;
                }
                Debug.Assert(guilds.Count == 1);

                var warBaskets = _warGuilds.Where(x => x.Guild.Id == guilds.First().Id && x.War == warSelect).Select(x => x.Basket).ToList();
                Debug.Assert(warBaskets.Count == 1);
                if (!warBaskets.Any())
                {
                    continue;
                }
                var basketofWar = warBaskets.FirstOrDefault();
                if (!basketSelect.Contains(basketofWar))
                {
                    totalsCalc.Add(new TableUserCalc(user, GetEmptyList()));
                    continue;
                }

                var userWars = _warDays.Where(w => w.User == user && w.War == warSelect).ToList();
                if (userWars.Count == 0)
                {
                    totalsCalc.Add(new TableUserCalc(user, GetEmptyList()));
                    continue;
                }
                var index = 0;
                foreach (var color in ColorUnits.All.GetByOneColor())
                {
                    var defenceId = userWars.Where(w => w.ColorDay == color).FirstOrDefault()?.Defence.Id ?? -1;
                    Debug.Assert(defenceId >= 0);
                    if (defenceId > 0)
                    {
                        var points = calcUserTable[color][defenceId];
                        list.Add(points);
                    }
                    else
                    {
                        list.Add(0);
                    }
                    index++;
                }
                totalsCalc.Add(new TableUserCalc(user, list));
            }
            totalsCalc = totalsCalc.OrderByDescending(x => x.Total).ToList();
            DataGridVisibility.SetOneVisibility(DataGridItemVisibility.TableDataGrid);

            CalculationStatistic.AddRange(totalsCalc);
            CalculationFiltredStatistic = CalculationStatistic;

        }

        private Dictionary<ColorUnits, Dictionary<int, double>> CalcDefencePercent()
        {
            var calcUserTable = new Dictionary<ColorUnits, Dictionary<int, double>>();
            const int minDefCount = 10;
            var mainStat = new WarDayMainStats(minDefCount, _warDays.ToArray());
            var defenceFilter = new DefenceFilter();
            foreach (var color in ColorUnits.All.GetByOneColor())
            {
                calcUserTable.Add(color, new Dictionary<int, double>());

                var colorFilter = new WarDayColorFilter(color);
                colorFilter.CreatePredicate();

                var warsFilter = new WarWarDayFilter(_wars);
                warsFilter.CreatePredicate();

                foreach (var defence in _defences)
                {
                    defenceFilter.Defence = defence;
                    defenceFilter.CreatePredicate();
                    var unitStat = mainStat
                        .SetFilter(defenceFilter)
                        .SetFilter(colorFilter)
                        .SetFilter(warsFilter);

                    var calcStat = unitStat.Calculate();
                    var statUnit = new StatDefence(calcStat, defence, false);
                    if (statUnit.Statictic.Count > 0)
                    {
                        var points = Math.Round(statUnit.Statictic.WinRate, 2);
                        calcUserTable[color][defence.Id] = points;
                    }
                }
            }

            return calcUserTable;
        }

        private List<double> GetEmptyList()
        {
            var list = new List<double>(6);
            for (int i = 0; i < 6; i++)
            {
                list.Add(0);
            }
            return list;
        }

        private void GetMatrixVictory()
        {
            _calculationStatistic.Clear();
            var mainStat = new AnabiosAttackMainStat(_realStates.ToArray());
            var guildFilter = new GuildRealStateFilter();

            foreach (var guild in _guilds.Skip(2))
            {
                if (guild.Name == "Sons of Anarchy")
                {
                    continue;
                }
                guildFilter.Guild = guild;
                guildFilter.CreatePredicate();

                var realStat = mainStat
                   .SetFilter(guildFilter);

                var calcStat = realStat.Calculate();
                var statGuild = new StatGuildReal(calcStat, guild);
                _calculationStatistic.Add(statGuild);
            }
            DataGridVisibility.SetOneVisibility(DataGridItemVisibility.StandartDataGrid);
            CalculationFiltredStatistic = CalculationStatistic;
        }

        private void OnUpdateUser()
        {
            _calculationStatistic.Clear();
            var mainStat = new WarDayMainStats(_warDays.ToArray());
            var defenceFilter = new UserFilter();
            var colorFilter = new WarDayColorFilter(ColorWarDay);
            colorFilter.CreatePredicate();

            var warsSelect = WarCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem);
            var warsFilter = new WarWarDayFilter(warsSelect);
            warsFilter.CreatePredicate();

            foreach (var item in _users)
            {
                defenceFilter.User = item;
                defenceFilter.CreatePredicate();
                var unitStat = mainStat
                    .SetFilter(defenceFilter)
                    .SetFilter(colorFilter);

                if (UseDifference)
                {
                    unitStat.SetFilterDifference(warsFilter);
                }
                else
                {
                    unitStat = unitStat.SetFilter(warsFilter);
                }

                var calcStat = unitStat.Calculate();
                var statUnit = new StatUser(calcStat, item, UseDifference);
                _calculationStatistic.Add(statUnit);
            }
            DataGridVisibility.SetOneVisibility(DataGridItemVisibility.StandartDataGrid);
            CalculationFiltredStatistic = CalculationStatistic;
        }

        private void OnUpdateDefence()
        {
            _calculationStatistic.Clear();
            var mainStat = new WarDayMainStats(_warDays.ToArray());
            var defenceFilter = new DefenceFilter();
            var colorFilter = new WarDayColorFilter(ColorWarDay);
            colorFilter.CreatePredicate();

            var warsSelect = WarCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem);
            var warsFilter = new WarWarDayFilter(warsSelect);
            warsFilter.CreatePredicate();
            ChangeDataGrid();
            foreach (var item in _defences)
            {
                defenceFilter.Defence = item;
                defenceFilter.CreatePredicate();
                var unitStat = mainStat
                    .SetFilter(defenceFilter)
                    .SetFilter(colorFilter);

                if (UseDifference)
                {
                    unitStat.SetFilterDifference(warsFilter);
                }
                else
                {
                    unitStat = unitStat.SetFilter(warsFilter);
                }

                if (!UseBasket)
                {
                    var calcStat = unitStat.Calculate();
                    var statUnit = new StatDefence(calcStat, item, UseDifference);
                    _calculationStatistic.Add(statUnit);
                }
                else
                {
                    
                    var statistics = new List<CalcStat>();
                    var calcStat = unitStat.Calculate();
                    statistics.Add(calcStat);

                    var basketFilter = new BasketsFilter(_warGuilds, _userInGuild);
                    var group1 = Group1DaysCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem).ToArray();
                    var group2 = Group2DaysCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem).ToArray();
                    var group3 = Group3DaysCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem).ToArray();
                    var groups = new [] {group1, group2, group3};
                    const int countGroup = 3;
                    for (int i = 0; i < countGroup; i++)
                    {
                        basketFilter.SetBaskets(groups[i]);
                        basketFilter.CreatePredicate();
                        var basketStat = unitStat.SetFilter(basketFilter);

                        var basketCalcStat = basketStat.Calculate();
                        statistics.Add(basketCalcStat);
                    }
                    var statDefence = new StatDefenceBasket(item, UseDifference, statistics.ToArray());
                    statDefence.BasketGroupCounts = 3;
                    _calculationStatistic.Add(statDefence);
                }
            }
            CalculationFiltredStatistic = CalculationStatistic;
        }

        private void OnUpdateUnit()
        {
            _calculationStatistic.Clear();
            var mainStat = new WarDayMainStats(_warDays.ToArray());
            var unitFilter = new UnitFilter();
            var colorFilter = new WarDayColorFilter(ColorWarDay);
            colorFilter.CreatePredicate();

            var warsSelect = WarCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem);
            var warsFilter = new WarWarDayFilter(warsSelect);
            warsFilter.CreatePredicate();

            var units = FiltredUnits(_units).ToArray();
            ChangeDataGrid();

            foreach (var item in units)
            {

                unitFilter.Unit = item;
                unitFilter.CreatePredicate();
                var unitStat = mainStat
                    .SetFilter(unitFilter)
                    .SetFilter(colorFilter);

                if (UseDifference)
                {
                    unitStat.SetFilterDifference(warsFilter);
                }
                else
                {
                    unitStat = unitStat.SetFilter(warsFilter);
                }

                if (!UseBasket)
                {
                    var calcStat = unitStat.Calculate();
                    var statUnit = new StatUnit(calcStat, item, UseDifference);
                    _calculationStatistic.Add(statUnit);
                }
                else
                {
                    var statistics = new List<CalcStat>();
                    var calcStat = unitStat.Calculate();
                    statistics.Add(calcStat);

                    var basketFilter = new BasketsFilter(_warGuilds, _userInGuild);
                    var baskets = BasketCalcViewModels.Where(x => x.IsChecked).Select(x => x.CheckItem);

                    foreach (var basket in baskets)
                    {
                        basketFilter.SetBaskets(basket);
                        basketFilter.CreatePredicate();
                        var basketStat = unitStat.SetFilter(basketFilter);

                        var basketCalcStat = basketStat.Calculate();
                        statistics.Add(basketCalcStat);
                    }

                    var statUnit = new StatUnitBasket(item, UseDifference, statistics.ToArray());
                    statUnit.Baskets = _baskets;
                    _calculationStatistic.Add(statUnit);
                }

            }

            CalculationFiltredStatistic = CalculationStatistic;
        }

        private void ChangeDataGrid()
        {
            if (!UseBasket)
            {
                DataGridVisibility.SetOneVisibility(DataGridItemVisibility.StandartDataGrid);
            }
            else
            {
                DataGridVisibility.SetOneVisibility(DataGridItemVisibility.LevelDataGrid);
            }
        }

        private void UseFilters()
        {
            _calculationFiltredStatistic.Clear();
            var clonestatistic = _calculationStatistic;
            if (QuantityFilterChecked)
            {
                clonestatistic = clonestatistic
                .Where(x => x.Count >= QuantityFilterValue)
                .ToObservableCollection();
            }
            if (NotEmptyValuesChecked)
            {
                clonestatistic = clonestatistic
                .Where(x => x.WinRate >= 0)
                .ToObservableCollection();
            }
            _calculationFiltredStatistic.AddRange(clonestatistic);
            RaisePropertyChanged(nameof(CalculationFiltredStatistic));
            if (!UseGroup)
            {
                _calculationFiltredGroupStatistic.Clear();
                _calculationFiltredGroupStatistic.AddRange(_calculationFiltredStatistic);
                return;
            }
        }


        private void OnUseGroup()
        {
            if (!UseGroup)
            {
                _calculationFiltredGroupStatistic.Clear();
                _calculationFiltredGroupStatistic.AddRange(_calculationFiltredStatistic);
                return;
            }
            var defenceStatistic = _calculationFiltredStatistic.OfType<StatDefence>();
            if (!defenceStatistic.Any())
            {
                return;
            }
            var groups = new List<List<StatDefence>>();
            foreach (var statDefence in defenceStatistic)
            {
                bool isAdd = false;
                foreach (var group in groups)
                {
                    var firstInGroup = group.First();
                    if (statDefence.Defence.BeAnalog(firstInGroup.Defence))
                    {
                        group.Add(statDefence);
                        isAdd = true;
                        break;
                    }
                }
                if (!isAdd)
                {
                    groups.Add(new List<StatDefence>(new[] { statDefence }));
                }
            }

            var defenceStatGroups = new ObservableCollection<BaseStatItem>();
            foreach (var groupStat in groups)
            {
                var defGroup = new DefenceGroupStatItem(groupStat);
                defGroup.CalculateNewStatistic();
                defenceStatGroups.Add(defGroup);

            }
            _calculationFiltredGroupStatistic = defenceStatGroups;

        }
        private void OnBufMax()
        {
            var isStatItemsCalculate = _calculationFiltredGroupStatistic
                .OfType<StatItem>().Any();
            var buffer = new StringBuilder();

            if (isStatItemsCalculate)
            {
                var ordered = _calculationFiltredGroupStatistic
                .OfType<StatItem>()
                .OrderBy(x => x.Statictic.LosRate);
              
                int index = 1;
                foreach (var item in ordered)
                {
                    buffer.Append(index.ToString());
                    buffer.Append(". \t");
                    buffer.Append(item.PrintValueDiff);
                    index++;
                }
            }
            else
            {
                var totalsCalc = _calculationFiltredStatistic
               .OfType<TableUserCalc>();
                var average = totalsCalc.Where(x => x.Total > 0).Select(x => x.Total).Average();
                const int percent = 10;
                var minValue = average * (100 - percent);
                var maxValue = average * (100 + percent);
                int itemIndex = 1;
                foreach (var item in totalsCalc)
                {
                    buffer.Append(itemIndex.ToString());
                    buffer.Append(". \t");
                    buffer.AppendLine(item.PrintValue);
                   
                    itemIndex++;
                }
                buffer.AppendLine($"Min value {minValue}\t Average {average} \t MaxValue {maxValue}");
            }

            Clipboard.SetText(buffer.ToString());
        }

        public override void Load()
        {
            Context.Database.CommandTimeout = 180;
            Context.Guilds.Load();
            _guilds = Context.Guilds.ToList();

            Context.RealWarStates
                .Include(x => x.War)
                .Include(x => x.EnemyGuild)
                .Load();
            _realStates = Context.RealWarStates.Local.ToList();

            Context.Units.Load();
            _units = Context.Units.ToList();

            Context.Users.Load();
            _users = Context.Users.ToList();

            Context.UsersInGuilds.Include(x => x.Guild).Include(x => x.User).Load();
            _userInGuild = Context.UsersInGuilds.Local.ToList();

            Context.WarDays.Include(x=>x.User).Load();
            Context.HeroClasses.Load();
            Context.Defences.Load();

            Context.Wars.Load();
            _wars = Context.Wars.OrderBy(x => x.DateStart).ToList();
            _warGuilds = Context.WarsGuilds.ToList();
            _baskets = Context.WarsGuilds.Select(x=>x.Basket).Distinct().ToArray();

            Context.Defences
                .Include(db => db.Units1)
                .Include(db => db.Units2)
                .Include(db => db.Units3)
                .Include(db => db.Units4)
                .Load();

            _defences = Context.Defences.Local.ToList();

            Context.WarDays
            .Include(db => db.Defence)
            .Include(db => db.Defence.Units1)
            .Include(db => db.Defence.Units2)
            .Include(db => db.Defence.Units3)
            .Include(db => db.Defence.Units4)
            .Include(db => db.User)
            .Load();

            _warDays = Context.WarDays.ToList();
            WarCalcViewModels = _wars.Select(x => new CheckedViewModel<War>(x)).ToList();
            BasketCalcViewModels = _baskets.Select(x=> new CheckedViewModel<int>(x)).ToList();
            Group1DaysCalcViewModels = _baskets.Select(x => new CheckedViewModel<int>(x)).ToList();
            Group2DaysCalcViewModels = _baskets.Select(x => new CheckedViewModel<int>(x)).ToList();
            Group3DaysCalcViewModels = _baskets.Select(x => new CheckedViewModel<int>(x)).ToList();
            SelectStandartGroup();
        }

        private void SelectStandartGroup()
        {
            foreach (var item in Group1DaysCalcViewModels)
            {
                if (item.CheckItem > 3)
                {
                    item.IsChecked = false;
                }
            }
            foreach (var item in Group2DaysCalcViewModels)
            {
                if (item.CheckItem <= 3 || item.CheckItem >= 6)
                {
                    item.IsChecked = false;
                }
            }
            foreach (var item in Group3DaysCalcViewModels)
            {
                if (item.CheckItem < 6)
                {
                    item.IsChecked = false;
                }
            }
        }
    }
}
