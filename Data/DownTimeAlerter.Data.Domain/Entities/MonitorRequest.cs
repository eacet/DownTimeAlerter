using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DownTimeAlerter.Data.Domain.Entities {
    [Table("MonitorRequests")]
    public class MonitorRequest : BaseEntity {
        [Required]
        public Guid MonitorId { get; set; }
        [Required]
        public short ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
