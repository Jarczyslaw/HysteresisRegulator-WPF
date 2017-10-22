using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HysteresisRegulator.Services
{
    public interface IDialogService
    {
        bool ShowResetSettingsDialog();
        bool ShowOkCancelDialog(string text);
        void ShowErrorDialog(string text);
    }
}
