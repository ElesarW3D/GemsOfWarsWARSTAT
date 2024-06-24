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

namespace SimpleControlLibrary.SelectControl.SixColorsControl
{
    /// <summary>
    /// Логика взаимодействия для ColorRectangle.xaml
    /// </summary>
    public partial class ColorRectangle : UserControl
    {
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public bool Selection
        {
            get { return (bool)GetValue(SelectionProperty); }
            set { SetValue(SelectionProperty, value); }
        }
        public bool Nonselection
        {
            get { return (bool)GetValue(NonselectionProperty); }
            set { SetValue(NonselectionProperty, value); }
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public int SquareSize
        {
            get { return (int)GetValue(SquareSizeProperty); }
            set { SetValue(SquareSizeProperty, value); }
        }
        
        public int BorderSize
        {
            get { return (int)GetValue(BorderSizeProperty); }
            set { SetValue(BorderSizeProperty, value); }
        }

        public event RoutedPropertyChangedEventHandler<bool> SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        public static readonly DependencyProperty SquareSizeProperty; 
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty SelectionProperty;
        public static readonly DependencyProperty NonselectionProperty;

        public static readonly DependencyProperty BorderSizeProperty;

        public static readonly DependencyProperty IsReadOnlyProperty;


        public static readonly RoutedEvent SelectionChangedEvent;

        static ColorRectangle()
        {
            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorRectangle), new PropertyMetadata(Colors.Aqua));
            SquareSizeProperty = DependencyProperty.Register("SquareSize", typeof(int), typeof(ColorRectangle), new PropertyMetadata(10));
            SelectionProperty = DependencyProperty.Register("Selection", typeof(bool), typeof(ColorRectangle), new PropertyMetadata(true));
            NonselectionProperty = DependencyProperty.Register("Nonselection", typeof(bool), typeof(ColorRectangle), new PropertyMetadata(false));
            BorderSizeProperty = DependencyProperty.Register("BorderSize", typeof(int), typeof(ColorRectangle), new PropertyMetadata(2));
            IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ColorRectangle), new PropertyMetadata(false,IsReadOnlyChange));
            SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ColorRectangle));
        }

        private static void IsReadOnlyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public ColorRectangle()
        {
            InitializeComponent();
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsReadOnly)
            {
                return;
            }

            Selection = !Selection;
            RoutedPropertyChangedEventArgs<bool> routedEventArgs = 
                new RoutedPropertyChangedEventArgs<bool>( 
                    oldValue:!Selection,
                    newValue:Selection,                
                    routedEvent: SelectionChangedEvent);
            RaiseEvent(routedEventArgs);
        }
    }
}
