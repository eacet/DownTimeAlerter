using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.Domain.Enums;
using DownTimeAlerter.Data.Domain.Models;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DownTimeAlerter.Business.Service.Services {

    /// <summary>
    /// Hangfire Service for Job Creation
    /// </summary>
    public class HangfireService : IHangfireService {

        public IMonitoringService MonitoringService { get; }
        public IMonitoringRequestService MonitoringRequestService { get; }
        public INotificationService NotificationService { get; }
        public ILogger<HangfireService> Logger { get; }

        public HangfireService(IMonitoringService monitoringService, IMonitoringRequestService monitoringRequestService, INotificationService notificationService, ILogger<HangfireService> logger) {
            MonitoringService = monitoringService;
            MonitoringRequestService = monitoringRequestService;
            NotificationService = notificationService;
            Logger = logger;
        }


        /// <summary>
        /// Create Monitoring Jobs
        /// </summary>
        public void CreateRecurringJobsForMonitorings() {
            try {
                var monitorings = MonitoringService.GetAllMonitoringsWithUserInfo();

                foreach (var monitoring in monitorings) {
                    string jobId = $"Monitoring - {monitoring.Id}";

                    RecurringJob.AddOrUpdate(
                        jobId,
                        () => RequestUrlAndSaveDb(monitoring),
                        Cron.MinuteInterval(monitoring.Interval)
                    );
                }
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at HangfireService.CreateRecurringJobsForMonitorings(), {ex}");
                throw;
            }
        }


        /// <summary>
        /// Making http request for monitoring url and save response results into database
        /// </summary>
        /// <param name="monitor"></param>
        /// <returns></returns>
        public async Task RequestUrlAndSaveDb(Monitor monitor) {
            try {

                HttpResponseMessage request = null;
                var monitorRequest = new MonitorRequest() {
                    MonitorId = monitor.Id
                };

                try {
                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(15);
                    request = await client.GetAsync(monitor.Url);

                    monitorRequest.ResponseCode = (short)request.StatusCode;
                    monitorRequest.ResponseMessage = request.StatusCode.ToString();
                    monitorRequest.IsSuccess = request.IsSuccessStatusCode;

                }
                catch (HttpRequestException requestException) {
                    monitorRequest.ResponseCode = 500;
                    monitorRequest.ResponseMessage = "ServerError";
                    monitorRequest.IsSuccess = false;
                }

                //Notification
                if (!monitorRequest.IsSuccess) {
                    var notificationModel = new NotificationModel {
                        DisplayName = "Down Time Alerter",
                        Subject = $"The Site Named {monitor.Name} Is Unreachable",
                        Message = $"The site named {monitor.Name} ({monitor.Url}) is unreachable. Response Code: {monitorRequest.ResponseCode}, Response Message: {monitorRequest.ResponseMessage}",
                        NotificationUserModel = new NotificationUserModel {
                            Mail = monitor.User.Email
                        }
                    };

                    NotificationService.Notify(notificationModel);
                }


                //Monitor Request Save
                MonitoringRequestService.Add(monitorRequest);
                MonitoringRequestService.Save();

                //Monitor Update
                UpdateMonitoringLastStatusAndLastCheckDate(monitor, monitorRequest.IsSuccess);

            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at HangfireService.RequestUrlAndSaveDb(Monitor monitor), {ex}");
            }
        }

        /// <summary>
        /// Update Monitoring Last Status and Last Check Date
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="isSuccess"></param>
        private void UpdateMonitoringLastStatusAndLastCheckDate(Monitor monitor, bool isSuccess) {
            try {
                var result = MonitoringService.GetById(monitor.Id);

                result.LastCheckDate = DateTime.Now;
                result.LastStatus = isSuccess ? MonitorStatus.Success : MonitorStatus.Fail;
                MonitoringService.Edit(result);
                MonitoringService.Save();
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at HangfireService.UpdateMonitoringLastStatusAndLastCheckDate(Monitor monitor, bool isSuccess), {ex}");
                throw;
            }
        }

    }
}
