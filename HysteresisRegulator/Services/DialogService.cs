using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HysteresisRegulator.Services
{
    public class DialogService : IDialogService
    {
        public bool ShowResetSettingsDialog()
        {
            return ShowOkCancelDialog("Do you really want to reset application settings?");
        }

        public bool ShowOkCancelDialog(string text)
        {
            var result = MessageBox.Show(text, "Question", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            return result == MessageBoxResult.OK;
        }
    }
}
