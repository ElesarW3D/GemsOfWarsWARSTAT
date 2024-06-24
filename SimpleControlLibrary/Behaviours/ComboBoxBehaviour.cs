using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SimpleControlLibrary.Behaviours
{
    public class ComboBoxBehaviour : Behavior<ComboBox>
    {
        private static ComboBox _oldCombobox;
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Drop += AssociatedObject_Drop;
            AssociatedObject.PreviewMouseRightButtonDown += AssociatedObject_PreviewMouseRightButtonDown;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Drop -= AssociatedObject_Drop;
            AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObject_PreviewMouseRightButtonDown;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            if (e.Data.GetData(DataFormats.StringFormat) is string droppedItem)
            {
                bool isShiftPressed = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
                var oldText = AssociatedObject.Text;
                if (isShiftPressed)
                {
                    if (_oldCombobox is ComboBox comboboxSender)
                    {
                        comboboxSender.Text = oldText;
                    }
                }
                AssociatedObject.Text = droppedItem;
            }
        }

        private void AssociatedObject_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.Text is string draggedItem)
                {
                    bool isShiftPressed = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

                    var effect = DragDropEffects.Copy;
                    if (isShiftPressed)
                    {
                        effect = DragDropEffects.Move;
                        _oldCombobox = comboBox;
                    }
                    else
                    {
                        _oldCombobox = null;
                    }
                    DataObject dataObject = new DataObject(DataFormats.StringFormat, draggedItem);
                    var result = DragDrop.DoDragDrop(comboBox, dataObject, effect);
                }
                
            }
            
        }

    }
}
