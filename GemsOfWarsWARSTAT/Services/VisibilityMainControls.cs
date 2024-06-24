using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static GemsOfWarsMainTypes.Extension.EnumExtension;

namespace GemsOfWarsWARSTAT.Services
{
    public enum VisiblityControls
    {
        DefenceAdd,
        UnitsAdd,
        UsersAdd,
        WarsAdd,
        WarDaysAdd,
        Statistic,
        RealStat,
        ChartStatistic,
        WarDayImport,
    }
    public class VisibilityMainControls : VisibilityOneEnumItem<VisiblityControls>
    {
        public VisibilityMainControls()
        {
        }

        public Visibility DefenceAddVisibility
        {
            get { return _controls[VisiblityControls.DefenceAdd]; }
        }

        public Visibility UnitsAddVisibility
        {
            get { return _controls[VisiblityControls.UnitsAdd]; }
        }
        public Visibility UsersAddVisibility
        {
            get { return _controls[VisiblityControls.UsersAdd]; }
        }

        public Visibility WarsAddVisibility
        {
            get { return _controls[VisiblityControls.WarsAdd]; }
        }

        public Visibility WarDaysAddVisibility
        {
            get { return _controls[VisiblityControls.WarDaysAdd]; }
        }

        public Visibility StatisticVisibility
        {
            get { return _controls[VisiblityControls.Statistic]; }
        }

        public Visibility RealStatVisibility
        {
            get { return _controls[VisiblityControls.RealStat]; }
        }

        public Visibility ChartStatisticVisibility
        {
            get { return _controls[VisiblityControls.ChartStatistic]; }
        }

        public Visibility WarDayImportVisibility
        {
            get { return _controls[VisiblityControls.WarDayImport]; }
        }
    }
}
