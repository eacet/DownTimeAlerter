using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.CommonModels.ViewModels;
using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.EF.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DownTimeAlerter.Business.Service.Services {

    /// <summary>
    /// Monitoring Service for Access Monitoring entities
    /// </summary>
    public class MonitoringService : BaseService<Monitor>, IMonitoringService {

        public IMonitoringRequestService MonitoringRequestService { get; }

        public MonitoringService(IMonitoringRepository repository, IMonitoringRequestService monitoringRequestService, ILogger<Monitor> logger)
            : base(repository, logger) {
            MonitoringRequestService = monitoringRequestService;
        }


        /// <summary>
        /// Edit Monitor 
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
            catch (Exception) {

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
                throw;
            }
        }
    }
}
