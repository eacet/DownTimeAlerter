using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.EF.IRepositories;
using Microsoft.Extensions.Logging;

namespace DownTimeAlerter.Business.Service.Services {

    /// <summary>
    /// Monitoring Request Service for Access Monitoring Requests
    /// </summary>
    public class MonitoringRequestService : BaseService<MonitorRequest>, IMonitoringRequestService {

        public MonitoringRequestService(IMonitoringRequestRepository repository, ILogger<MonitorRequest> logger)
            : base(repository, logger) { }
    }
}
