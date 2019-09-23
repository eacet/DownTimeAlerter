using System.Collections.Generic;
using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.EF.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DownTimeAlerter.Data.EF.Repositories {

    /// <summary>
    /// Monitoring Repository for Access Monitoring entities
    /// </summary>
    public class MonitoringRepository : BaseRepository<Monitor>, IMonitoringRepository {
        public MonitoringRepository(DbContext context)
            : base(context) { }

        /// <summary>
        /// Get All Monitorings with User Information
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Monitor> GetAllMonitoringsWithUserInfo() {
            return Dbset.Include(x => x.User).ToList();
        }
    }
}
