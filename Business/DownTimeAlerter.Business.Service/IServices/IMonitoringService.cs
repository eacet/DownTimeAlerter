using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.Domain.Models;
using System;
using System.Collections.Generic;

namespace DownTimeAlerter.Business.Service.IServices {
    /// <summary>
    /// Monitoring Service for Access Monitoring entities
    /// </summary>
    public interface IMonitoringService : IBaseService<Monitor> {

        /// <summary>
        /// Get All Monitorings with User Information
        /// </summary>
        /// <returns></returns>
        IEnumerable<Monitor> GetAllMonitoringsWithUserInfo();

        /// <summary>
        /// Create new Monitor Entity
        /// </summary>
        /// <param name="model"></param>
        void Add(MonitoringViewModel model);

        /// <summary>
        /// Edit Monitor Entity
        /// </summary>
        /// <param name="model"></param>
        void Edit(MonitoringViewModel model);

        /// <summary>
        /// Get Monitoring Detail By Monitor Id and User Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        MonitoringDetailViewModel GetMonitoringDetail(Guid id, Guid userId);
    }
}
