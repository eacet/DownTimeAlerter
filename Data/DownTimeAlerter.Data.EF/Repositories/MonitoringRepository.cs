using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.EF.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DownTimeAlerter.Data.EF.Repositories {

    /// <summary>
    /// Web Site Repository for access Web Sites
    /// </summary>
    public class MonitoringRepository : BaseRepository<Monitor>, IMonitoringRepository {
        public MonitoringRepository(DbContext context)
            : base(context) { }
    }
}
