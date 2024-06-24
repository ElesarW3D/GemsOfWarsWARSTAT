using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarAnalitics;
using GemsOfWarAnalitics.Extension;
using GemsOfWarAnalitics.MainStats;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsMainTypes.SubType;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsWARSTAT.Services;
using ScottplotLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GemsOfWarsMainTypes.GlobalConstants;
using DColor = System.Drawing.Color;
using System.Windows.Media;
using GemsOfWarAnalitics.Filter;
using System.Diagnostics;

namespace GemsOfWarsWARSTAT.ViewModel.Statistic
{
    public class ChartStatisticViewModel : BaseVisualViewModel
    {
        private ObservableCollection<Scatter> _chartDate;
        private ObservableCollection<Unit> _units;
        private List<RealWarState> _realStates;
        private List<WarDay> _warDays;
        private List<WarGuild> _warGuilds;
        private List<War> _war;
        private Unit _selection;
        private ColorUnits _colorWarDay = ColorUnits.All;
        private Color _colorMain;
        private Color _colorOther;
        private string _YChartLegend;
        private string _XChartLegend;

        public override VisiblityControls ViewModelType => VisiblityControls.ChartStatistic;
        public ObservableCollection<Scatter> ChartDate { get => _chartDate; set => _chartDate = value; }
        public List<Scatter> CalculatePoints => ChartDate.ToList();
        public ColorUnits ColorWarDay
        {
            get => _colorWarDay;
            set
            {
                _colorWarDay = value;
                RaisePropertyChanged(nameof(ColorWarDay));
            }
        }
      

        public Unit Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                RaisePropertyChanged(nameof(Selection));
            }
        }

        public ObservableCollection<Unit> Units
        {
            get { return _units; }
            set
            {
                if (_units != value)
                {
                    _units = value;
                    RaisePropertyChanged(nameof(Units));
                }
            }
        }

        public RelayCommand PrepareData { get; set; }
        public RelayCommand AddData { get; set; }
        public RelayCommand DeleteData { get; set; }
        public RelayCommand GuildVictoryPercent { get; set; }
        
        public RelayCommand ChartByDay { get; set; }
        public RelayCommand CalcPairState { get; set; }
        public RelayCommand CalcDefenceEfficiency { get; set; }
        public Color ColorMain 
        { 
            get => _colorMain;
            set
            {
                _colorMain = value;
                RaisePropertyChanged(nameof(ColorMain));
            }
        }

        public Color ColorOther 
        { 
            get => _colorOther;
            set
            {
                _colorOther = value;
                RaisePropertyChanged(nameof(ColorOther));
            }
        }

        public string YChartLegend
        {
            get => _YChartLegend;
            set
            {
                _YChartLegend = value;
                RaisePropertyChanged(nameof(YChartLegend));
            }
        }

        public string XChartLegend
        {
            get => _XChartLegend;
            set
            {
                _XChartLegend = value;
                RaisePropertyChanged(nameof(XChartLegend));
            }
        }

        public ChartStatisticViewModel(WarDbContext dbContext) : base(dbContext)
        {
            _chartDate = new ObservableCollection<Scatter>();
            PrepareData = new RelayCommand(OnPrepareData);
            AddData = new RelayCommand(OnAddData, OnHasSelection);
            DeleteData = new RelayCommand(OnDeleteData, OnDeleteCanExecute);
            GuildVictoryPercent = new RelayCommand(OnCalcGuildVictory);
            ChartByDay = new RelayCommand(OnChartByDay, OnHasSelection);
            CalcPairState = new RelayCommand(OnCalcPairState, CalcPairStateCanExecute);
            CalcDefenceEfficiency = new RelayCommand(OnDefenceEfficiency);
        }

        private bool OnHasSelection()
        {
            return Selection.IsNotNull();
        }

        private void OnDefenceEfficiency()
        {
            SetDateToXAndToY("Эффективность защит");
            var charDatas = new List<Scatter>();
            var fullStat = new AnabiosDefenceMainStat(_realStates.ToArray());
            fullStat.Strategy = AnabiosDefenceStrategy.DefenceEfficiency;
            var counter = 0;
            var colors = ColorWarDay;
            var colorFilter = new ColorRealStatFilter(colors);
            colorFilter.CreatePredicate();
            var colorFiltred = fullStat.SetFilter(colorFilter);
            foreach (var war in _war)
            {
                var basket = _warGuilds.Where(w => w.War.Id == war.Id && w.Guild.Id == 1).FirstOrDefault()?.Basket ?? -1;
                var warFilter = new WarRealStatFilter();
                warFilter.AddWarToFilter(war);
                warFilter.CreatePredicate();
                var selectWar = colorFiltred.SetFilter(warFilter);
                var calc = selectWar.Calculate();
                var defenceEfficiency = calc.WinRate;


                var fullText = DefenceEfficensy.Args(war.DateStart.ToString("dd MM"), basket, defenceEfficiency);


                var chartpointOne = new Scatter(new double[] { counter }, new[] { defenceEfficiency }, new[] { fullText }, DColor.DarkGoldenrod);

                charDatas.Add(chartpointOne);
                counter++;
            }
            ChartDate.AddRange(charDatas);

        }
        private bool CalcPairStateCanExecute()
        {
            return Selection.IsNotNull();
        }
        private void OnCalcPairState()
        {
            SetDateToXAndToY(Selection.DisplayName);

            var fullStat = new WarDayMainStats(_warDays.ToArray());
            
            var unitFilterMain = new UnitFilter();
            unitFilterMain.Unit = Selection;
            unitFilterMain.CreatePredicate();

            var calcItems = fullStat.SetFilter(unitFilterMain);
            var charDatas = new List<Scatter>();
            var counter = 0;
            foreach (var unit in Units)
            {
                if (unit == Selection)
                {
                    continue;
                }
                
                var unitFilterSecond = new UnitFilter();
                unitFilterSecond.Unit = unit;
                unitFilterSecond.CreatePredicate();
                var secondCalc = calcItems.SetFilter(unitFilterSecond);
                var calculation = secondCalc.Calculate();
                if (calculation.Count < 1)
                {
                    continue;
                }
                var text = $"{unit.DisplayName}\n WR = {calculation.WinRate:P2} \n Count = {calculation.Count}";
                var chartpoints = new Scatter(
                      new double[] { counter },
                      new[] { calculation.WinRate },
                      new[] { text });
                counter++;
                charDatas.Add(chartpoints);
            }

            var orderedCharts = charDatas.OrderByDescending(it => it.Y.FirstOrDefault()).ToArray();
            int x = 0;
            foreach (var chart in orderedCharts)
            {
                chart.X[0] = x;
                x++;
            }
            ChartDate.Clear();
            ChartDate.AddRange(orderedCharts);
        }
        private void OnChartByDay()
        {
            
            SetDateToXAndToY(Selection.DisplayName);
            
            var fullStat = new WarDayMainStats(_warDays.ToArray());
            var warStat = new WarDayMainStats(_warDays.ToArray());
            var unitFilter = new UnitFilter();
            unitFilter.Unit = Selection;
            unitFilter.CreatePredicate();

            var colors = ColorWarDay.GetByOneColor();
            var charDatas = new List<Scatter>();
            var dic = new Dictionary<int, string>();
            dic.Add(0, "Все вместе\n");
            foreach (var color in colors)
            {
                var warDayColorFilter = new WarDayColorFilter(color);
                warDayColorFilter.CreatePredicate();

                var fullwarsFilter = new WarWarDayFilter(_war);
                var counter = 0;

                counter = AddFullStatistic(fullStat, unitFilter, fullwarsFilter, warDayColorFilter, charDatas, counter, color);
                
                foreach (var war in _war)
                {
                    counter++;
                    var OnewarsFilter = new WarWarDayFilter();
                    OnewarsFilter.AddWarToFilter(war);
                    OnewarsFilter.CreatePredicate();
                    var OnewarStat = warStat
                        .SetFilter(unitFilter)
                        .SetFilter(warDayColorFilter)
                        .SetFilter(OnewarsFilter);

                    var OnecalcStat = OnewarStat.Calculate();
                    var OneWinRate = OnecalcStat.GetPercentWinRate();
                    
                    //if (!dic.ContainsKey(counter))
                    //{
                    //    dic.Add(counter, WarSplitNameTab.Args(war.DateStart.ToString("dd MM"), war.Basket) + "\n");
                    //}
                    
                    var text = OnecalcStat.Count > 0? AttackColorCount.Args(color.ToString(), OnecalcStat.Count) :"";
                    var chartpoints = new Scatter(
                        new double[] { counter },
                        new[] { OneWinRate },
                        new[] { text },
                        color.GetLightColor()
                        );
                    charDatas.Add(chartpoints);
                }
            }
            var orderedCharts = charDatas.OrderByDescending(x => x.Y.FirstOrDefault()).ToArray();

            foreach (var item in dic)
            {
                var scatter =  orderedCharts.Where(x=>x.X.First() == item.Key);
                var firstScatter = scatter.First();
                firstScatter.Titles[0] = item.Value + firstScatter.Titles[0];
            }
            ChartDate.Clear();
            ChartDate.AddRange(orderedCharts);
        }

        private void OnCalcGuildVictory()
        {
            ChartDate.Clear();
            SetDateToXAndToY("Проценты побед защиты");
            var charDatas = new List<Scatter>();
            var fullStat = new AnabiosDefenceMainStat(_realStates.ToArray());
            var fullcalcStat = fullStat.Calculate();
            var fullWinRate = fullcalcStat.GetPercentWinRate();
            var textFull = $"Все вместе\n стычки:{ fullcalcStat.Count}";
            var chartpointFull = new Scatter(new double[] { 0 }, new[] { fullWinRate }, new[] { textFull }, DColor.BlueViolet);

            charDatas.Add(chartpointFull);
            
            var counter = 1;
            var OneStat = new AnabiosDefenceMainStat(_realStates.ToArray());
            foreach (var war in _war)
            {
                var basket = _warGuilds.Where(w => w.War.Id == war.Id && w.Guild.Id == 1).FirstOrDefault()?.Basket ?? -1;
                var warFilter = new WarRealStatFilter();
                warFilter.AddWarToFilter(war);
                warFilter.CreatePredicate();
                OneStat.Strategy = AnabiosDefenceStrategy.CalcEnemy;
                var filterOneState = OneStat.SetFilter(warFilter);
                var oneCalcStat = filterOneState.Calculate();
                var defWinRate = oneCalcStat.GetPercentWinRate();
               

                
                OneStat.Strategy = AnabiosDefenceStrategy.CalcAnabios;
                var filterOneStateA = OneStat.SetFilter(warFilter);
                var oneCalcStatA = filterOneStateA.Calculate();
                var defWinRateA = oneCalcStatA.GetPercentWinRate();

                var fullText = WarSplitNameTabFull.Args(war.DateStart.ToString("dd MM"), basket, oneCalcStat.Count, defWinRate, oneCalcStatA.Count, defWinRateA);

                
                var chartpointOne = new Scatter(new double[] { counter }, new[] { defWinRate }, new[] { fullText }, DColor.Aquamarine);
                
                var chartpointOneA = new Scatter(new double[] { counter }, new[] { defWinRateA }, new[] { fullText }, DColor.YellowGreen);
                if (defWinRate > defWinRateA)
                {
                    charDatas.Add(chartpointOne);
                    charDatas.Add(chartpointOneA);
                }
                else
                {
                    charDatas.Add(chartpointOneA);
                    charDatas.Add(chartpointOne);
                }
                counter++;
            }
            ChartDate.AddRange(charDatas);
        }

        private void SetDateToXAndToY(string y)
        {
            XChartLegend = "Даты войны";
            YChartLegend = y;
        }

        private void OnDeleteData()
        {
            ChartDate.Clear();
        }

        private bool OnDeleteCanExecute()
        {
            return ChartDate.Any();
        }

        private void OnAddData()
        {
            if (Selection.IsNull())
            {
                return;
            }
            SetDateToXAndToY(Selection.DisplayName);
            var fullStat = new WarDayMainStats(_warDays.ToArray());
            var warStat = new WarDayMainStats(_warDays.ToArray());
            var unitFilter = new UnitFilter();
            unitFilter.Unit = Selection;
            unitFilter.CreatePredicate();

            var warDayColorFilter = new WarDayColorFilter(ColorWarDay);
            warDayColorFilter.CreatePredicate();

            var fullwarsFilter = new WarWarDayFilter(_war);
            var charDatas = new List<Scatter>();
            var counter = 0;

            counter = AddFullStatistic(fullStat, unitFilter, fullwarsFilter, warDayColorFilter, charDatas, counter, ColorWarDay.GetDarkColor());

            foreach (var war in _war)
            {
                counter++;
                var OnewarsFilter = new WarWarDayFilter();
                OnewarsFilter.AddWarToFilter(war);
                OnewarsFilter.CreatePredicate();
                var OnewarStat = warStat
                    .SetFilter(unitFilter)
                    .SetFilter(warDayColorFilter)
                    .SetFilter(OnewarsFilter);

                var OnecalcStat = OnewarStat.Calculate();
                var OneWinRate = OnecalcStat.GetPercentWinRate();
                var text = " 123";//WarSplitNameTab2.Args(war.DateStart.ToString("dd MM"), war.Basket, OnecalcStat.Count);
                var chartpoints = new Scatter(
                    new double[] { counter },
                    new[] { OneWinRate },
                    new[] { text },
                    ColorWarDay.GetLightColor()
                    );
                charDatas.Add(chartpoints);
            }

            ChartDate.AddRange(charDatas);
        }

        private void OnPrepareData()
        {
            SetDateToXAndToY(Selection.DisplayName);
            var fullStat = new WarDayMainStats(_warDays.ToArray());
            var warStat = new WarDayMainStats(_warDays.ToArray());
            var unitFilter = new UnitFilter();
            unitFilter.Unit = Selection;
            unitFilter.CreatePredicate();

            var warDayColorFilter = new WarDayColorFilter(ColorWarDay);
            warDayColorFilter.CreatePredicate();

            var fullwarsFilter = new WarWarDayFilter(_war);
            var charDatas = new List<Scatter>();
            var counter = 0;

            counter = AddFullStatistic(fullStat, unitFilter, fullwarsFilter, warDayColorFilter, charDatas, counter, DColor.BlueViolet);

            foreach (var war in _war)
            {
                counter++;
                var OnewarsFilter = new WarWarDayFilter();
                OnewarsFilter.AddWarToFilter(war);
                OnewarsFilter.CreatePredicate();
                var OnewarStat = warStat
                    .SetFilter(unitFilter)
                    .SetFilter(warDayColorFilter)
                    .SetFilter(OnewarsFilter);

                var OnecalcStat = OnewarStat.Calculate();
                var OneWinRate = OnecalcStat.GetPercentWinRate();
                var text = "123";// WarSplitNameTab2.Args(war.DateStart.ToString("dd MM"), war.Basket, OnecalcStat.Count);
                var chartpoints = new Scatter(
                    new double[] { counter },
                    new[] { OneWinRate },
                    new[] { text },
                    DColor.Aquamarine
                    );
                charDatas.Add(chartpoints);
            }


            ChartDate.Clear();
            ChartDate.AddRange(charDatas);
        }

        private static int AddFullStatistic(
            WarDayMainStats fullStat, 
            UnitFilter unitFilter, 
            WarWarDayFilter fullwarsFilter, 
            WarDayColorFilter warDayColorFilter, 
            List<Scatter> charDatas, 
            int counter,
            DColor color )
        {
            fullwarsFilter.CreatePredicate();
            var unitStatFull = fullStat
                .SetFilter(unitFilter)
                .SetFilter(warDayColorFilter)
                .SetFilter(fullwarsFilter);
            var fullCalcStat = unitStatFull.Calculate();
            counter++;
            var winRateFull = fullCalcStat.GetPercentWinRate();
            var textFull = $"Все вместе\n стычки:{ fullCalcStat.Count}";
            var chartpointFull = new Scatter(new double[] { counter }, new[] { winRateFull }, new[] { textFull }, color);
            charDatas.Add(chartpointFull);
            return counter;
        }

        private static int AddFullStatistic(
           WarDayMainStats fullStat,
           UnitFilter unitFilter,
           WarWarDayFilter fullwarsFilter,
           WarDayColorFilter warDayColorFilter,
           List<Scatter> charDatas,
           int counter,
           ColorUnits color)
        {
            fullwarsFilter.CreatePredicate();
            var unitStatFull = fullStat
                .SetFilter(unitFilter)
                .SetFilter(warDayColorFilter)
                .SetFilter(fullwarsFilter);
            var fullCalcStat = unitStatFull.Calculate();
            var winRateFull = fullCalcStat.GetPercentWinRate();
            var textFull = fullCalcStat.Count>0?$"стычки ({color}):{ fullCalcStat.Count}":"";
            var chartpointFull = new Scatter(new double[] { counter }, new[] { winRateFull }, new[] { textFull }, color.GetDarkColor());
            charDatas.Add(chartpointFull);
            return counter;
        }

        public override void Load()
        {
            Context.Database.CommandTimeout = 180;
            Context.Units.Load();
            Context.HeroClasses.Load();
            Units = Context.Units.Local.ToArray().OrderByDisplayName().ToObservableCollection();


            Context.Wars.Load();
            _war = Context.Wars.OrderBy(x => x.DateStart).ToList();

            Context.WarsGuilds.Include(x=>x.Guild).Include(x=>x.War).Load();
            _warGuilds = Context.WarsGuilds.ToList();

            Context.WarDays
            .Include(db => db.Defence)
            .Include(db => db.Defence.Units1)
            .Include(db => db.Defence.Units2)
            .Include(db => db.Defence.Units3)
            .Include(db => db.Defence.Units4)
            .Include(db => db.User)
            .Load();
            _warDays = Context.WarDays.ToList();

            Context.RealWarStates
              .Include(x => x.War)
              .Include(x => x.EnemyGuild)
              .Load();
            _realStates = Context.RealWarStates.Local.ToList();
        }

    }
}
