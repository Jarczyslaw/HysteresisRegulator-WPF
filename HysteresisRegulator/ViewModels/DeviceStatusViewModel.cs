using DeviceCommunication;
using DeviceCommunication.Device;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HysteresisRegulator.ViewModels
{
    public class DeviceStatusViewModel : ViewModelBase
    {
        private Communication communication;

        public DeviceStatusViewModel(Communication communication)
        {
            this.communication = communication;
            communication.Pooling.UpdateStatus += (s) => Status = s;
            RefreshCommand = new RelayCommand(Refresh);
        }

        private void Refresh()
        {
            Status = communication.Reader.GetStatus();
        }

        public RelayCommand RefreshCommand { get; private set; }

        private DeviceStatus status;
        public DeviceStatus Status
        {
            get { return status; }
            set { Set(() => this.Status, ref status, value); }
        }
    }
}
