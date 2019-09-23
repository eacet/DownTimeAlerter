using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.EF.Repositories;
using Xunit;

namespace DownTimeAlerter.Tests {
    public class MonitoringRepositoryTest : IClassFixture<InMemoryTestFixture> {
        private readonly InMemoryTestFixture _fixture;
        public MonitoringRepositoryTest(InMemoryTestFixture fixture) {
            _fixture = fixture;
        }

        /// <summary>
        /// Monitoring Create Test
        /// </summary>
        [Fact]
        public void ShouldAddNewMonitoring() {
            // Arrange 
            var repository = new MonitoringRepository(_fixture.Context);
            var monitor = new Monitor() { Name = "OK" };

            // Act
            repository.Add(monitor);
            repository.Save();

            var count = repository.GetAll();

            //Assert
            Assert.Single(count);

        }

        /// <summary>
        /// Monitoring Update Test
        /// </summary>
        [Fact]
        public void ShouldUpdateMonitoring() {
            // Arrange 
            var repository = new MonitoringRepository(_fixture.Context);
            var monitor = new Monitor() { Name = "OK" };

            // Act
            repository.Add(monitor);
            repository.Save();

            monitor.Name = "Update";

            repository.Edit(monitor);
            repository.Save();

            var result = repository.GetById(monitor.Id);

            //Assert
            Assert.Equal("Update", result.Name);

        }


        /// <summary>
        /// Monitoring Delete Test
        /// </summary>
        [Fact]
        public void ShouldRemoveMonitoring() {
            // Arrange 
            var repository = new MonitoringRepository(_fixture.Context);
            var monitor = new Monitor() { Name = "OK" };

            // Act
            repository.Add(monitor);
            repository.Save();

            monitor = new Monitor() { Name = "OK" };
            repository.Add(monitor);
            repository.Save();

            repository.Delete(monitor);


            var count = repository.GetAll();


            //Assert
            Assert.Single(count);
        }
    }
}
