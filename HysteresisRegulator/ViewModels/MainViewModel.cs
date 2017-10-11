using ApplicationSettings;
using DeviceCommunication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HysteresisRegulator.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ChartsViewModel ChartsVM { get; private set; }
        public ConnectionViewModel ConnectionVM { get; private set; }
        public DeviceStatusViewModel DeviceStatusVM { get; private set; }
        public SettingsViewModel SettingsVM { get; private set; }

        private Communication communication;
        private AppSettings appSettings;

        public MainViewModel()
        {
            appSettings = new AppSettings();
            communication = new Communication();

            ChartsVM = new ChartsViewModel(appSettings, communication);
            ConnectionVM = new ConnectionViewModel(appSettings, communication);
            DeviceStatusVM = new DeviceStatusViewModel(communication);
            SettingsVM = new SettingsViewModel(appSettings, communication);

            CloseCommand = new RelayCommand(Close);
        }

        private void Close()
        {
            App.Current.Shutdown();
        }

        public RelayCommand CloseCommand { get; private set; }
    }
}
