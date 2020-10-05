using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inflow.Data
{
    public class TimerBackgroundWorks : BackgroundService
    {
        public static StringBuilder TimerWorkLog { get; set; } = new StringBuilder();

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Timer timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
            return Task.CompletedTask;
        }
        private void DoWork(Object state)
        {
            if (TimerWorkLog.Length > 2000) TimerWorkLog.Clear();
            TimerWorkLog.AppendLine($"timer work on {DateTime.Now}");
        }
    }
}
