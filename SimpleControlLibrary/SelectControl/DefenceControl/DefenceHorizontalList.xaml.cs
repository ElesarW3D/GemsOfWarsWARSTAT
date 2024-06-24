using System;
using System.Collections.Generic;
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

namespace SimpleControlLibrary.SelectControl.DefenceControl
{
    /// <summary>
    /// Логика взаимодействия для DefenceHorizontalList.xaml
    /// </summary>
    public partial class DefenceHorizontalList : UserControl
    {
        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }


        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(DefenceHorizontalList), new PropertyMetadata(0));


        public DefenceHorizontalList()
        {
            InitializeComponent();
        }
    }
}
