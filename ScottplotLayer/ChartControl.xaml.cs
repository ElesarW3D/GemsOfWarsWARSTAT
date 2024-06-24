using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScottplotLayer
{
    public enum ScatterType
    {
        Line,
        Bar
    }
    /// <summary>
    /// Логика взаимодействия для ChartControl.xaml
    /// </summary>
    public partial class ChartControl : UserControl
    {


        public ScatterType ScatterType
        {
            get { return (ScatterType)GetValue(ScatterTypeProperty); }
            set { SetValue(ScatterTypeProperty, value); }
        }

        
        public string XChartLegend
        {
            get { return (string)GetValue(XChartLegendProperty); }
            set { SetValue(XChartLegendProperty, value); }
        }

        public string YChartLegend
        {
            get { return (string)GetValue(YChartLegendProperty); }
            set { SetValue(YChartLegendProperty, value); }
        }

        public static readonly DependencyProperty YChartLegendProperty =
            DependencyProperty.Register("YChartLegend", typeof(string), typeof(ChartControl), new PropertyMetadata(""));
        public static readonly DependencyProperty XChartLegendProperty =
          DependencyProperty.Register("XChartLegend", typeof(string), typeof(ChartControl), new PropertyMetadata(""));
        
        public static readonly DependencyProperty ScatterTypeProperty =
            DependencyProperty.Register("ScatterType", typeof(ScatterType), typeof(ChartControl), new PropertyMetadata(ScatterType.Line));


        public static readonly DependencyProperty ChartDatasProperty =
        DependencyProperty.Register(
          name: "ChartDatas",
          propertyType: typeof(ObservableCollection<Scatter>),
          ownerType: typeof(ChartControl)
        );

        public ObservableCollection<Scatter> ChartDatas
        {
            get { return (ObservableCollection<Scatter>)GetValue(ChartDatasProperty); }
            set { SetValue(ChartDatasProperty, value); }
        }
     
        public ChartControl()
        {
            InitializeComponent();
           
            SetValue(ChartDatasProperty, new ObservableCollection<Scatter>());
            RecalculateData();
        }

        private void RecalculateData()
        {
            var plot = Chart.Plot;
            plot.Clear();
            AddDataToPlot(plot);

            if (string.IsNullOrEmpty(XChartLegend))
            {
                plot.XLabel("Horizontal Axis");
            }
            else
            {
                plot.XLabel(XChartLegend);
            }
            if (string.IsNullOrEmpty(XChartLegend))
            {
                plot.YLabel("Vertical Axis");
            }
            else
            {
                plot.YLabel(YChartLegend);
            }
            if (ChartDatas.Any())
            {
                Chart.Refresh();
            }
        }

        private void AddDataToPlot(ScottPlot.Plot plot)
        {
            if (!ChartDatas.Any())
            {
                return;
            }
           
            HashSet<double> yvalues = new HashSet<double>();
            Dictionary<double, string> xTitles = new Dictionary<double, string>();

            foreach (var scatter in ChartDatas)
            {
                InitScatter(plot, scatter);
                yvalues.AddRange(scatter.Y);
                AddXtitlesFromScatter(xTitles, scatter);
            }

            InitTicks(plot, xTitles.Keys.ToArray(), xTitles.Values.ToArray());
            SetAxisLimited(plot, yvalues.ToArray());
        }

        private void AddXtitlesFromScatter(Dictionary<double, string> xTiles, Scatter scatter)
        {
            for (int i = 0; i < scatter.X.Length; i++)
            {
                if (scatter.Titles.Length < i)
                {
                    continue;
                }
                var title = scatter.Titles[i];
                if (string.IsNullOrEmpty(title))
                {
                    continue;
                }
                var x = scatter.X[i];
                if(xTiles.ContainsKey(x))
                {
                    var tiles = xTiles[x].Split(new[] { "\n\r" }, StringSplitOptions.RemoveEmptyEntries);
                    if (!tiles.Contains(title))
                    {
                        xTiles[x] += "\n\r" + title;
                    }
                }
                else
                {
                    xTiles.Add(x, title);
                }
            }
        }

        private void InitScatter(ScottPlot.Plot plot, Scatter scatter)
        {
            switch (ScatterType)
            {
                case ScatterType.Line:
                    plot.AddScatter(scatter.Y, scatter.X, scatter.Color);
                    break;
                case ScatterType.Bar:
                    plot.AddBar(scatter.Y, scatter.X, scatter.Color);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
           
        }

        private static void InitTicks(ScottPlot.Plot plot, double[] X, string[] titles)
        {
            plot.XTicks(X, titles);
        }

        private static void SetAxisLimited(ScottPlot.Plot plot, double[] dataY)
        {
            var YMin = dataY.Min();
            var yplaceMin = YMin * 0.75;
            var YMax = dataY.Max();
            var yplaceMax = YMax * 1.2;
            plot.SetAxisLimitsY(yplaceMin, yplaceMax);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RecalculateData();
        }
    }
}
