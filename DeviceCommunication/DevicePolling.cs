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
                return new int[] { 1, 2, 5, 10 };
            }
        }

        private int interval;
        public int Interval
        {
            get { return interval; }
            set
            {
                if (Intervals.Contains(value))
                    interval = value;
                else
                    interval = Intervals[0];
            }
        }

        private DeviceReader reader;

        private CancellationTokenSource tokenSource;
        private Task poolingTask;

        public DevicePolling(DeviceReader reader)
        {
            this.reader = reader;
            Interval = Intervals[0];
        }

        public void Start()
        {
            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            poolingTask = Task.Run(async () =>
            {
                while(!token.IsCancellationRequested)
                {
                    try
                    {
                        var status = reader.GetStatus();
                        UpdateStatus?.Invoke(status);
                    }
                    catch { }
                    await Task.Delay(Interval * 1000, token).ContinueWith(t => { });
                }
            }, token);
        }

        public void Stop()
        {
            tokenSource?.Cancel();
            poolingTask?.Wait();
        }
    }
}
