using System;

namespace DownTimeAlerter.Data.Domain.Models {
    public class MonitoringViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public Guid UserId { get; set; }
    }
}
