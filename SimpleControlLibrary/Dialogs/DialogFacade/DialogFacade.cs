using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SimpleControlLibrary.Dialogs.DialogSave;
using SimpleControlsLibrary.Dialogs.DialogService;
using SimpleControlsLibrary.Dialogs.DialogYesNo;

namespace SimpleControlsLibrary.Dialogs.DialogFacade
{
    public class DialogFacade : IDialogFacade
    {
        public string FileName { get; private set; }
        public DialogFacade()
        {
        }

        public CustomDialogResult ShowDialogSave(string message, Window owner)
        {
            var vm = new DialogSaveViewModel(message, new DialogSaveView());
            var result = ShowDialog(vm, owner);
            FileName = vm.FileName;
            return result;
        }

        public CustomDialogResult ShowDialogYesNo(string message, Window owner)
        {
            DialogViewModelBase vm = new DialogYesNoViewModel(message, new DialogYesNoView());
            return this.ShowDialog(vm, owner);
        }

        private CustomDialogResult ShowDialog(DialogViewModelBase vm, Window owner)
        {
            DialogWindow win = new DialogWindow();
            if (owner != null)
                win.Owner = owner;
            win.DataContext = vm;
            win.ShowDialog();
            var result =
                (win.DataContext as DialogViewModelBase).UserDialogResult;
            return result;
        }

        public CustomDialogResult ShowDialogImport(string message,string filename, Window owner)
        {
            var vm = new DialogImportViewModel(message, new DialogImportView());
            vm.FileName = filename;
            var result = ShowDialog(vm, owner);
            FileName = vm.FileName;
            return result;
        }
    }
}
