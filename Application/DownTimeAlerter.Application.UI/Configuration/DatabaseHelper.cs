using DownTimeAlerter.Data.Domain.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DownTimeAlerter.Application.UI.Configuration {
    /// <summary>
    /// Database Helper
    /// </summary>
    public static class DatabaseHelper {

        /// <summary>
        /// Migrate Db Context
        /// </summary>
        /// <param name="app"></param>
        public static void MigrateDb(this IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices.CreateScope()) {
                var context = serviceScope.ServiceProvider.GetService<DownTimeAlerterDbContext>();

                context.Database.Migrate();
            }
        }
    }
}
