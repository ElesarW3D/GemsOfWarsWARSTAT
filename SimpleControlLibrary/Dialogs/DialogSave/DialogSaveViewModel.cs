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
    class DialogSaveViewModel : DialogViewModelBase
    {
        private ICommand _cancelCommand = null;
        private ICommand _saveCommand = null;
        private string _fileName;

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set { _cancelCommand = value; }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
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

        public DialogSaveViewModel(string message, object content)
            : base(message, content)
        {
           CancelCommand = new RelayCommand<object>(OnYesClicked);
           SaveCommand = new RelayCommand<object>(OnSaveCommand);
        }

        private void OnYesClicked(object parameter)
        {
            this.CloseDialogWithResult(parameter as Window, CustomDialogResult.No);
        }

        private void OnSaveCommand(object parameter)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = saveFileDialog.FileName;
                this.CloseDialogWithResult(parameter as Window, CustomDialogResult.Yes);
                return;
            }
            this.CloseDialogWithResult(parameter as Window, CustomDialogResult.No);
        }
    }
}
