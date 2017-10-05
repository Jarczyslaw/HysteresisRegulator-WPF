using ApplicationSettings;
using DeviceCommunication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HysteresisRegulator.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            appSettings = new AppSettings();
            communication = new Communication();

            ChartsVM = new ChartsViewModel(appSettings);
            ConnectionVM = new ConnectionViewModel(communication);
            DeviceStatusVM = new DeviceStatusViewModel();
            SettingsVM = new SettingsViewModel(appSettings);

            ShowHelpCommand = new RelayCommand(ShowHelp);
        }

        private void ShowHelp()
        {
            
        }

        public ChartsViewModel ChartsVM { get; private set; }
        public ConnectionViewModel ConnectionVM { get; private set; }
        public DeviceStatusViewModel DeviceStatusVM { get; private set; }
        public SettingsViewModel SettingsVM { get; private set; }

        private Communication communication;
        private AppSettings appSettings;

        public RelayCommand ShowHelpCommand { get; private set; }
    }
}
