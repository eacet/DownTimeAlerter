using System;

namespace DownTimeAlerter.Data.CommonModels.ViewModels {
    public class MonitoringViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
    }
}
