using DownTimeAlerter.Data.Domain.Entities;
using System.Collections.Generic;

namespace DownTimeAlerter.Data.EF.IRepositories {
    /// <summary>
    /// Monitoring Repository for Access Monitoring entities
    /// </summary>
    public interface IMonitoringRepository : IBaseRepository<Monitor> {
        /// <summary>
        /// Get All Monitorings with User Information
        /// </summary>
        /// <returns></returns>
        IEnumerable<Monitor> GetAllMonitoringsWithUserInfo();
    }
}
