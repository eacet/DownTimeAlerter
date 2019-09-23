using DownTimeAlerter.Data.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DownTimeAlerter.Data.Domain.Entities {
    [Table("Monitors")]
    public class Monitor : BaseEntity {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int Interval { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime LastCheckDate { get; set; }
        public MonitorStatus LastStatus { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
