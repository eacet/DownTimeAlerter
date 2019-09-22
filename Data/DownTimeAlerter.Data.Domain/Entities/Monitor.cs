using DownTimeAlerter.Data.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DownTimeAlerter.Data.Domain.Entities {
    [Table("Monitors")]
    public class Monitor : BaseEntity {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime LastCheckDate { get; set; }
        public MonitorStatus LastStatus { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public ICollection<MonitorRequest> MonitorRequests { get; set; }
    }
}
