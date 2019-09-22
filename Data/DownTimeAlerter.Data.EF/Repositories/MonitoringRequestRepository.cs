using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.EF.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DownTimeAlerter.Data.EF.Repositories {
    public class MonitoringRequestRepository : BaseRepository<MonitorRequest>, IMonitoringRequestRepository {
        public MonitoringRequestRepository(DbContext context) : base(context) { }
    }
}
