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

        public ConnectionViewModel(Communication communication)
        {
            this.communication = communication;

            ConnectCommand = new RelayCommand(Connect);
            RefreshPortsCommand = new RelayCommand(RefreshPorts);
            RefreshPorts();
            SelectedPort = Ports.First();

            Test = "ASD";
        }

        private void RefreshPorts()
        {
            Ports = communication.GetPorts();
        }

        private void Connect()
        {
            Debug.WriteLine("Connect to: " + SelectedPort);
        }

        public RelayCommand ConnectCommand { get; private set; }
        public RelayCommand RefreshPortsCommand { get; private set; }

        private string test;
        public string Test
        {
            get { return test; }
            set { Set(() => Test, ref test, value); }
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
            set { Set(() => SelectedPort, ref selectedPort, value); }
        }
    }
}
