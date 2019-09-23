using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DownTimeAlerter.Tests {
    public class MonitoringRequestRepositoryTest : IClassFixture<InMemoryTestFixture> {
        private readonly InMemoryTestFixture _fixture;
        public MonitoringRequestRepositoryTest(InMemoryTestFixture fixture) {
            _fixture = fixture;
        }

        /// <summary>
        /// Monitoring Request Create Test
        /// </summary>
        [Fact]
        public void ShouldAddNewMonitoringRequest() {
            // Arrange 
            var repository = new MonitoringRequestRepository(_fixture.Context);
            var monitorRequest = new MonitorRequest() { MonitorId = Guid.NewGuid(), ResponseCode = 200 };

            // Act
            repository.Add(monitorRequest);
            repository.Save();

            var count = repository.GetAll();

            //Assert
            Assert.Single(count);

        }

        /// <summary>
        /// Monitoring Request Update Test
        /// </summary>
        [Fact]
        public void ShouldUpdateMonitoringRequest() {
            // Arrange 
            var repository = new MonitoringRequestRepository(_fixture.Context);
            var monitorRequest = new MonitorRequest() { MonitorId = Guid.NewGuid(), ResponseCode = 200 };

            // Act
            repository.Add(monitorRequest);
            repository.Save();

            monitorRequest.ResponseCode = 500;

            repository.Edit(monitorRequest);
            repository.Save();

            var result = repository.GetById(monitorRequest.Id);

            //Assert
            Assert.Equal(500, result.ResponseCode);

        }


        /// <summary>
        /// Monitoring Request Delete Test
        /// </summary>
        [Fact]
        public void ShouldRemoveMonitoringRequest() {
            // Arrange 
            var repository = new MonitoringRequestRepository(_fixture.Context);
            var monitorRequest = new MonitorRequest() { MonitorId = Guid.NewGuid(), ResponseCode = 200 };

            // Act
            repository.Add(monitorRequest);
            repository.Save();

            monitorRequest = new MonitorRequest() { MonitorId = Guid.NewGuid(), ResponseCode = 200 };
            repository.Add(monitorRequest);
            repository.Save();

            repository.Delete(monitorRequest);


            var count = repository.GetAll();


            //Assert
            Assert.Single(count);
        }
    }
}
