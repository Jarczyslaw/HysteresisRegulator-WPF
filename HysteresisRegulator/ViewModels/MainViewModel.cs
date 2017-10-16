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
        public DeviceConnectionViewModel DeviceConnectionVM { get; private set; }
        public DeviceStatusViewModel DeviceStatusVM { get; private set; }
        public DeviceSettingsViewModel DeviceSettingsVM { get; private set; }

        private Communication communication;
        private AppSettings appSettings;

        public MainViewModel()
        {
            appSettings = new AppSettings();
            communication = new Communication();

            communication.CommunicationStart += Communication_CommunicationStart;
            communication.CommunicationStop += Communication_CommunicationStop;
            Communication_CommunicationStop();

            communication.Writer.WritesChanged += (i) => WritesCount = i;
            communication.Reader.ReadsChanged += (i) => ReadsCount = i;

            ChartsVM = new ChartsViewModel(appSettings, communication);
            DeviceConnectionVM = new DeviceConnectionViewModel(appSettings, communication);
            DeviceStatusVM = new DeviceStatusViewModel(communication);
            DeviceSettingsVM = new DeviceSettingsViewModel(appSettings, communication);

            CloseCommand = new RelayCommand(Close);
        }

        private void Communication_CommunicationStop()
        {
            ConnectedStatus = "Disconnected";
        }

        private void Communication_CommunicationStart()
        {
            ConnectedStatus = string.Format("Connected to: {0}", communication.PortName);
        }

        private void Close()
        {
            App.Current.Shutdown();
        }

        private int readsCount;
        public int ReadsCount
        {
            get { return readsCount; }
            set { Set(() => ReadsCount, ref readsCount, value); }
        }

        private int writesCount;
        public int WritesCount
        {
            get { return writesCount; }
            set { Set(() => WritesCount, ref writesCount, value); }
        }

        private string connectedStatus;
        public string ConnectedStatus
        {
            get { return connectedStatus; }
            set { Set(() => ConnectedStatus, ref connectedStatus, value); }
        }

        public RelayCommand CloseCommand { get; private set; }
    }
}
