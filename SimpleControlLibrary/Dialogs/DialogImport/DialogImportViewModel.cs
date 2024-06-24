using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleControlsLibrary.Dialogs.DialogService;

namespace SimpleControlsLibrary.Dialogs.DialogYesNo
{
    class DialogImportViewModel : DialogViewModelBase
    {
        private ICommand _cancelCommand = null;
        private ICommand _importCommand = null;
        private ICommand _reopenCommand = null;
        private string _fileName;

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set { _cancelCommand = value; }
        }

        public ICommand ImportCommand
        {
            get { return _importCommand; }
            set { _importCommand = value; }
        }

        public ICommand ReopenCommand
        {
            get { return _reopenCommand; }
            set { _reopenCommand = value; }
        }

        public string FileName 
        { 
            get => _fileName;
            set
            {
                _fileName = value;
                RaisePropertyChanged(nameof(FileName));
            }
        }

        public DialogImportViewModel(string message, object content)
            : base(message, content)
        {
           CancelCommand = new RelayCommand<object>(OnCancelCommand);
           ImportCommand = new RelayCommand<object>(OnImportCommand);
           ReopenCommand = new RelayCommand<object>(OnReopenCommand, CanReopen);
        }

        private bool CanReopen(object arg)
        {
            return !string.IsNullOrEmpty(FileName);
        }

        private void OnReopenCommand(object parameter)
        {
            this.CloseDialogWithResult(parameter as Window, CustomDialogResult.Yes);
        }

        private void OnCancelCommand(object parameter)
        {
            this.CloseDialogWithResult(parameter as Window, CustomDialogResult.No);
        }

        private void OnImportCommand(object parameter)
        {
            var openfileDialog = new FolderBrowserDialog();
            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openfileDialog.SelectedPath;
                this.CloseDialogWithResult(parameter as Window, CustomDialogResult.Yes);
                return;
            }
            this.CloseDialogWithResult(parameter as Window, CustomDialogResult.No);
        }
    }
}
