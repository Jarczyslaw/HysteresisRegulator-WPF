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
    public class DeviceConnectionViewModel : ViewModelBase
    {
        private Communication communication;
        private AppSettings appSettings;

        public DeviceConnectionViewModel(AppSettings settings, Communication communication)
        {
            this.communication = communication;
            this.appSettings = settings;
            this.appSettings.OnReset += () => LoadSettings();

            communication.CommunicationStart += Communication_CommunicationStart;
            communication.CommunicationStop += Communication_CommunicationStop;

            ConnectCommand = new RelayCommand(Connect);
            RefreshPortsCommand = new RelayCommand(RefreshPorts);
            RefreshPorts();
            LoadSettings();
        }

        public void LoadSettings()
        {
            SelectedPort = appSettings.SerialPort;
            SelectedInterval = appSettings.PoolingInterval;
        }

        private void Communication_CommunicationStop()
        {
            Connected = false;
        }

        private void Communication_CommunicationStart()
        {
            Connected = true;
        }

        private void RefreshPorts()
        {
            Ports = communication.GetPorts();
        }

        private void Connect()
        {
            Debug.WriteLine("Connect to: " + SelectedPort);
            if (!communication.Connected)
                communication.Start(SelectedPort);
            else
                communication.Stop();
        }

        private bool connected;
        public bool Connected
        {
            get { return connected; }
            set { Set(() => Connected, ref connected, value); }
        }

        private string[] ports;
        public string[] Ports
        {
            get { return ports; }
            set { Set(() => Ports, ref ports, value); }
        }

        private string selectedPort;
        public string SelectedPort
        {
            get { return selectedPort; }
            set
            {
                string newPort = value;
                if (!Ports.Contains(newPort))
                    newPort = Ports.FirstOrDefault();
                else
                    appSettings.SerialPort = newPort;
                Set(() => SelectedPort, ref selectedPort, newPort);
            }
        }

        private int selectedInterval;
        public int SelectedInterval
        {
            get { return selectedInterval; }
            set
            {
                Set(() => SelectedInterval, ref selectedInterval, value);
                communication.Pooling.Interval = value;
                appSettings.PoolingInterval = value;
            }
        }

        public int[] PoolingIntervals
        {
            get { return communication.Pooling.Intervals; }
        }

        public RelayCommand ConnectCommand { get; private set; }
        public RelayCommand RefreshPortsCommand { get; private set; }
    }
}
