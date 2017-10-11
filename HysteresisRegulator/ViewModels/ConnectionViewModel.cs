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
    public class ConnectionViewModel : ViewModelBase
    {
        private Communication communication;
        private AppSettings settings;

        public ConnectionViewModel(AppSettings settings, Communication communication)
        {
            this.communication = communication;
            this.settings = settings;

            ConnectCommand = new RelayCommand(Connect);
            RefreshPortsCommand = new RelayCommand(RefreshPorts);
            RefreshPorts();
            SelectedPort = settings.SerialPort;
            SelectedInterval = settings.PoolingInterval;
        }

        private void RefreshPorts()
        {
            Ports = communication.GetPorts();
        }

        private void Connect()
        {
            Debug.WriteLine("Connect to: " + SelectedPort);
            communication.Start(SelectedPort);
        }

        private string connected;
        public string Connected
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
                    settings.SerialPort = newPort;
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
                settings.PoolingInterval = value;
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
