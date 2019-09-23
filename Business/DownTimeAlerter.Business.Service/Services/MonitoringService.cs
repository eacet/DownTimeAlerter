using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.Domain.Models;
using DownTimeAlerter.Data.EF.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DownTimeAlerter.Business.Service.Services {

    /// <summary>
    /// Monitoring Service for Access Monitoring entities
    /// </summary>
    public class MonitoringService : BaseService<Monitor>, IMonitoringService {
        public IMonitoringRepository Repository { get; }
        public IMonitoringRequestService MonitoringRequestService { get; }

        public MonitoringService(IMonitoringRepository repository, IMonitoringRequestService monitoringRequestService, ILogger<Monitor> logger)
            : base(repository, logger) {
            Repository = repository;
            MonitoringRequestService = monitoringRequestService;
        }

        /// <summary>
        /// Get All Monitorings with User Information
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Monitor> GetAllMonitoringsWithUserInfo() {
            var results = Repository.GetAllMonitoringsWithUserInfo();
            return results;
        }

        /// <summary>
        /// Create new Monitor Entity
        /// </summary>
        /// <param name="model"></param>
        public void Add(MonitoringViewModel model) {
            try {
                var entity = new Monitor {
                    Name = model.Name,
                    Url = model.Url,
                    Interval = model.Interval,
                    UserId = model.UserId
                };

                Repository.Add(entity);
                Repository.Save();
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringService.Edit(MonitoringViewModel model), {ex}");
                throw;
            }
        }

        /// <summary>
        /// Edit Monitor Entity
        /// </summary>
        /// <param name="entity"></param>
        public void Edit(MonitoringViewModel model) {
            try {
                var entity = GetById(model.Id);
                entity.Name = model.Name;
                entity.Url = model.Url;
                entity.Interval = model.Interval;
                entity.UpdatedDate = DateTime.Now;
                Repository.Edit(entity);
                Repository.Save();
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringService.Edit(MonitoringViewModel model), {ex}");
                throw;
            }
        }

        /// <summary>
        /// Get Monitoring Detail By Monitor Id and User Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MonitoringDetailViewModel GetMonitoringDetail(Guid id, Guid userId) {
            try {
                var monitoring = Repository.GetBy(x => x.Id == id && x.UserId == userId);
                var successCount = MonitoringRequestService.CountBy(x => x.MonitorId == id && x.IsSuccess);
                var failCount = MonitoringRequestService.CountBy(x => x.MonitorId == id && !x.IsSuccess);


                var result = new MonitoringDetailViewModel {
                    Name = monitoring.Name,
                    Url = monitoring.Url,
                    Interval = monitoring.Interval,
                    CreatedDate = monitoring.CreatedDate,
                    LastCheckDate = monitoring.LastCheckDate,
                    LastStatus = monitoring.LastStatus,
                    SuccessCount = successCount,
                    FailCount = failCount
                };
                return result;
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringService.GetMonitoringDetail(Guid id, Guid userId), {ex}");
                throw;
            }
        }


    }
}
