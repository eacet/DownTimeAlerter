using DownTimeAlerter.Data.Domain.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownTimeAlerter.Application.UI.Configuration {
    public static class DatabaseHelper {
        public static void MigrateDb(this IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices.CreateScope()) {
                var context = serviceScope.ServiceProvider.GetService<DownTimeAlerterDbContext>();

                context.Database.Migrate();
            }
        }
    }
}
