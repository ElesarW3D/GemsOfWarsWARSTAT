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
using GemsOfWarsMainTypes.Extension;

namespace SimpleControlLibrary.SelectControl.SixColorsControl
{
    /// <summary>
    /// Логика взаимодействия для SixColorsEditor.xaml
    /// </summary>
    public partial class SixColorsEditor : UserControl
    {
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public bool IsOneColor
        {
            get { return (bool)GetValue(IsOneColorProperty); }
            set { SetValue(IsOneColorProperty, value); }
        }

        public int SelectColors
        {
            get { return (int)GetValue(SelectColorsProperty); }
            set { SetValue(SelectColorsProperty, value); }
        }

        public int BorderSize
        {
            get { return (int)GetValue(BorderSizeProperty); }
            set { SetValue(BorderSizeProperty, value); }
        }

        public int SquareSize
        {
            get { return (int)GetValue(SquareSizeProperty); }
            set { SetValue(SquareSizeProperty, value); }
        }

        static SixColorsEditor()
        {
            BorderSizeProperty =
                DependencyProperty.Register("BorderSize", typeof(int), typeof(SixColorsEditor), new PropertyMetadata(2));
            SquareSizeProperty = 
                DependencyProperty.Register("SquareSize", typeof(int), typeof(SixColorsEditor), new PropertyMetadata(10));
            SelectColorsProperty =
                DependencyProperty.Register("SelectColors", typeof(int), typeof(SixColorsEditor), new PropertyMetadata(0, ChildrenSelectionChange));

            IsReadOnlyProperty = 
                DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(SixColorsEditor), new PropertyMetadata(false));

            IsOneColorProperty =
                DependencyProperty.Register("IsOneColor", typeof(bool), typeof(SixColorsEditor), new PropertyMetadata(false));

    }

    private static void ChildrenSelectionChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
            int newColors = (int)e.NewValue;
            SixColorsEditor editor = (SixColorsEditor)d;
        //public enum ColorUnits
        //{
        //    Red = 1,
        //    Brown = 2,
        //    Purple = 4,
        //    Yellow = 8,
        //    Green = 16,
        //    Blue = 32,
        //}
            var colorRectangles = new[]
            {
                editor.MRed,
                editor.MBrown,
                editor.MPurple,
                editor.MYellow,
                editor.MGreen,
                editor.MBlue,
            };
            bool isOneColor = editor.IsOneColor;
            bool setColor = false;
            for (int i = 1,j=0; i <= 32; i*=2, j++)
            {
                var colorRectangle = colorRectangles[j];
                if (isOneColor)
                {
                    SetSelectionByOneColor(newColors, i, colorRectangle, ref setColor);
                }
                else
                {
                    SetSelectionByAnyColor(newColors, i, colorRectangle);
                }
            }
        }

        private static void SetSelectionByOneColor(int newColors, int i, ColorRectangle colorRectangle, ref bool setColor)
        {
            if ((newColors & i) == 0 || setColor)
            {
                colorRectangle.Selection = false;
            }
            else
            {
                colorRectangle.Selection = true;
                setColor = true;
            }
        }

        private static void SetSelectionByAnyColor(int newColors, int i, ColorRectangle colorRectangle)
        {
            if ((newColors & i) == 0)
            {
                colorRectangle.Selection = false;
            }
            else
            {
                colorRectangle.Selection = true;
            }
        }

        public static readonly DependencyProperty BorderSizeProperty;
        public static readonly DependencyProperty SquareSizeProperty;
        public static readonly DependencyProperty SelectColorsProperty;
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly DependencyProperty IsOneColorProperty;

        public SixColorsEditor()
        {
            InitializeComponent();
            
        }

        private void ColorRectangle_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (e.OriginalSource is ColorRectangle colorRectangle)
            {
                var value = Convert.ToInt32(colorRectangle.Tag.ToString());
                if (IsOneColor)
                {
                    SelectColors = value;
                }
                else
                {
                    SelectColors ^= value;
                }
            }
        }
    }
}
