using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Business.Service.Services;
using DownTimeAlerter.Data.Domain.Context;
using DownTimeAlerter.Data.EF.IRepositories;
using DownTimeAlerter.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DownTimeAlerter.Application.UI.Configuration {
    public static class IocService {
        public static void RegisterDependencies(this IServiceCollection services) {
            services.AddScoped(typeof(DbContext), typeof(DownTimeAlerterDbContext));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<INotificationService, MailService>();
            services.AddScoped<IMonitoringService, MonitoringService>();
            services.AddScoped<IMonitoringRepository, MonitoringRepository>();
            services.AddScoped<IMonitoringRequestService, MonitoringRequestService>();
            services.AddScoped<IMonitoringRequestRepository, MonitoringRequestRepository>();
            services.AddScoped<IHangfireService, HangfireService>();
        }
    }
}
