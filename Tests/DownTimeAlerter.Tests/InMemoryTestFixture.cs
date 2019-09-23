using DownTimeAlerter.Data.Domain.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownTimeAlerter.Tests
{

    public class InMemoryTestFixture : IDisposable {
        public DownTimeAlerterDbContext Context => InMemoryContext();

        public void Dispose() {
            Context?.Dispose();
        }

        private static DownTimeAlerterDbContext InMemoryContext() {
            var options = new DbContextOptionsBuilder<DownTimeAlerterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new DownTimeAlerterDbContext(options);

            return context;
        }
    }


}
