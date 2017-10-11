using DeviceCommunication.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceCommunication
{
    public class DevicePolling
    {
        public delegate void UpdateStatusHandler(DeviceStatus status);
        public event UpdateStatusHandler UpdateStatus;


        public int[] Intervals
        {
            get
            {
                return new int[] { 500, 1000, 2000, 5000 };
            }
        }

        public int Interval { get; set; } = 1000;

        private DeviceReader reader;

        private CancellationTokenSource tokenSource;
        private Task poolingTask;

        public DevicePolling(DeviceReader reader)
        {
            this.reader = reader;
        }

        public void Start()
        {
            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            poolingTask = Task.Run(async () =>
            {
                while(!token.IsCancellationRequested)
                {
                    var status = reader.GetStatus();
                    UpdateStatus?.Invoke(status);
                    await Task.Delay(Interval);
                }
            }, token);
        }

        public void Stop()
        {

        }
    }
}
