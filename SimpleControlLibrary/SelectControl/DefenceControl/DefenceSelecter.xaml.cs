using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SimpleControlLibrary.SelectControl.DefenceControl
{
    public partial class DefenceSelecter : UserControl
    {
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public object ItemSelected
        {
            get { return GetValue(ItemSelectedProperty); }
            set { SetValue(ItemSelectedProperty, value); }
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
            DependencyProperty.Register("Text", typeof(string), typeof(DefenceSelecter), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ColorValueProperty =
           DependencyProperty.Register("ColorValue", typeof(int), typeof(DefenceSelecter), new PropertyMetadata(1));

        public static readonly DependencyProperty ItemSelectedProperty =
            DependencyProperty.Register("MyProperty", typeof(object), typeof(DefenceSelecter), new PropertyMetadata(null));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DefenceSelecter), new PropertyMetadata(true));

        public DefenceSelecter()
        {
            InitializeComponent();
        }
    }
}
