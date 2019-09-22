using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DownTimeAlerter.Data.Domain.Entities {
    [Table("MonitorRequests")]
    public class MonitorRequest : BaseEntity {
        public Guid MonitorId { get; set; }
        public short ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
