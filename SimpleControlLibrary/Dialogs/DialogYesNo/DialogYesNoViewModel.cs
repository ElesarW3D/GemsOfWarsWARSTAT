using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleControlsLibrary.Dialogs.DialogService;

namespace SimpleControlsLibrary.Dialogs.DialogYesNo
{
    class DialogYesNoViewModel : DialogViewModelBase
    {
        private ICommand yesCommand = null;
        public ICommand YesCommand
        {
            get { return yesCommand; }
            set { yesCommand = value; }
        }

        private ICommand noCommand = null;
        public ICommand NoCommand
        {
            get { return noCommand; }
            set { noCommand = value; }
        }

        public DialogYesNoViewModel(string message, object content)
            : base(message, content)
        {
            this.yesCommand = new RelayCommand<object>(OnYesClicked);
            this.noCommand = new RelayCommand<object>(OnNoClicked);
        }

        private void OnYesClicked(object parameter)
        {
            this.CloseDialogWithResult(parameter as Window, CustomDialogResult.Yes);
        }

        private void OnNoClicked(object parameter)
        {
            this.CloseDialogWithResult(parameter as Window, CustomDialogResult.No);
        }
    }
}
