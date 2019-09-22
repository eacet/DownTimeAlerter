using DownTimeAlerter.Data.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DownTimeAlerter.Application.UI.Data {
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DownTimeAlerterDbContext> {
        public DownTimeAlerterDbContext CreateDbContext(string[] args) {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<DownTimeAlerterDbContext>();
            var connectionString = configuration.GetConnectionString("Default");
            builder.UseSqlServer(connectionString);
            return new DownTimeAlerterDbContext(builder.Options);
        }
    }
}
