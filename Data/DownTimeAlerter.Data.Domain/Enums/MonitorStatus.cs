using System.ComponentModel;

namespace DownTimeAlerter.Data.Domain.Enums {
    public enum MonitorStatus : byte {

        [Description("None")]
        None = 0,

        [Description("Requesting")]
        Requesting = 1,

        [Description("Request return 2xx code")]
        Success = 2,

        [Description("Request return except 2xx code")]
        Fail = 3
    }
}
