using ApplicationSettings;
using DeviceCommunication;
using DeviceCommunication.Device;
using GalaSoft.MvvmLight;
using HysteresisRegulator.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HysteresisRegulator.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        public ChartsData Data { get; private set; }
        public ChartsConfiguration Configuration { get; private set; }

        private AppSettings appSettings;
        private Communication communication;

        public ChartsViewModel(AppSettings appSettings, Communication communication)
        {
            Configuration = new ChartsConfiguration(appSettings);
            Data = new ChartsData(Configuration);
            this.appSettings = appSettings;
            this.communication = communication;

            communication.Pooling.UpdateStatus += (s) => Data.Push(s.Temperature, s.Setpoint, s.RelayState);
        }
    }
}
