using DeviceCommunication;
using DeviceCommunication.Device;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HysteresisRegulator.Services;
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
        private IDialogService dialogService;

        public DeviceStatusViewModel(Communication communication, IDialogService dialogService)
        {
            this.communication = communication;
            communication.Pooling.UpdateStatus += (s) => Status = s;
            this.dialogService = dialogService;
            RefreshCommand = new RelayCommand(Refresh, RefreshEnabled);
        }

        private async void Refresh()
        {
            try
            {
                Status = await communication.Reader.GetStatusAsync();
            }
            catch(Exception ex)
            {
                dialogService.ShowErrorDialog(ex.Message);
            }
        }

        private bool RefreshEnabled()
        {
            return communication.Connected;
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
