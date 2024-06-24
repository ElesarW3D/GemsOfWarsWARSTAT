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
    /// Логика взаимодействия для DefencePrinter.xaml
    /// </summary>
    public partial class DefencePrinter : UserControl
    {
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public int ColorValue
        {
            get { return (int)GetValue(ColorValueProperty); }
            set { SetValue(ColorValueProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

     
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DefencePrinter), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ColorValueProperty =
           DependencyProperty.Register("ColorValue", typeof(int), typeof(DefencePrinter), new PropertyMetadata(1));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DefencePrinter), new PropertyMetadata(false, IsReadOnlyChange));
        
        private static void IsReadOnlyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public DefencePrinter()
        {
            InitializeComponent();
        }
    }
}
