using DownTimeAlerter.Data.Domain.Enums;
using System;

namespace DownTimeAlerter.Data.Domain.Models {
    public class MonitoringDetailViewModel {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastCheckDate { get; set; }
        public MonitorStatus LastStatus { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
    }
}
