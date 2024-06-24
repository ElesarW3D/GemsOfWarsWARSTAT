using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SimpleControlsLibrary.Dialogs.DialogService
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        private string _message;
        private object _content;
        public CustomDialogResult UserDialogResult
        {
            get;
            private set;
        }

        public string Message
        {
            get => _message;
            private set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }

        public object Content
        {
            get => _content;
            private set
            {
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        public DialogViewModelBase(string message, object content)
        {
            Message = message;
            Content = content;
        }

        public void CloseDialogWithResult(Window dialog, CustomDialogResult result)
        {
            this.UserDialogResult = result;
            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
