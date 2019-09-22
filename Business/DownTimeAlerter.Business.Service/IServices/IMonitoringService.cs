using DownTimeAlerter.Data.CommonModels.ViewModels;
using DownTimeAlerter.Data.Domain.Entities;
using System;

namespace DownTimeAlerter.Business.Service.IServices {
    public interface IMonitoringService : IBaseService<Monitor> {
        MonitoringDetailViewModel GetMonitoringDetail(Guid id, Guid userId);
        void Edit(MonitoringViewModel model);
    }
}
