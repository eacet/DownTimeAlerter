using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.EF.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace DownTimeAlerter.Infrastructure.Common.Helper {

    /// <summary>
    /// Dependency Injection Helper
    /// </summary>
    public static class DependencyInjectionHelper {
        /// <summary>
        /// Extension method for Register All Services and Repositories into the .Net Core Injection. 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServiceAndRepositoryTypes(this IServiceCollection services) {
            var assemblies = new List<Assembly>
            {
                Assembly.GetAssembly(typeof (IMonitoringService)), // Service
                Assembly.GetAssembly(typeof (IMonitoringRepository)), // Repository
            };

            foreach (var assembly in assemblies) {
                foreach (var type in assembly.GetTypes()) {
                    if ((type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsInterface) {
                        services.AddScoped(
                           type.GetInterface(type.GetInterfaces()[type.GetInterfaces().Length - 1].Name),
                           type
                        );
                    }
                }
            }
        }
    }
}
