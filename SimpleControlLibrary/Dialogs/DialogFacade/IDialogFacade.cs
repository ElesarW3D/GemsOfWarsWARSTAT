using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SimpleControlsLibrary.Dialogs.DialogService;

namespace SimpleControlsLibrary.Dialogs.DialogFacade
{
    public interface IDialogFacade
    {
        CustomDialogResult ShowDialogYesNo(string message, Window owner);

        CustomDialogResult ShowDialogSave(string message, Window owner);
        CustomDialogResult ShowDialogImport(string message, string fileName, Window owner);

    }
}
