using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Business.Service.Model;
using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.Domain.Enums;
using Hangfire;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DownTimeAlerter.Business.Service.Services {
    public class HangfireService : IHangfireService {

        public IMonitoringService MonitoringService { get; }
        public IMonitoringRequestService MonitoringRequestService { get; }
        public INotificationService NotificationService { get; }

        public HangfireService(IMonitoringService monitoringService, IMonitoringRequestService monitoringRequestService, INotificationService notificationService) {
            MonitoringService = monitoringService;
            MonitoringRequestService = monitoringRequestService;
            NotificationService = notificationService;
        }



        public void CreateRecurringJobsForMonitorings() {
            var monitorings = MonitoringService.GetAll();

            foreach (var monitoring in monitorings) {
                string jobId = $"{monitoring.Name} - {monitoring.Id}";

                RecurringJob.RemoveIfExists(jobId);

                RecurringJob.AddOrUpdate(
                    jobId,
                    () => RequestUrlAndSaveDb(monitoring),
                    Cron.MinuteInterval(monitoring.Interval)
                );
            }
        }

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

                //Monitor Request Save
                MonitoringRequestService.Add(monitorRequest);
                MonitoringRequestService.Save();


                //Monitor Update
                UpdateMonitoringLastStatusAndLastCheckDate(monitor, monitorRequest.IsSuccess);


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


            }
            catch (Exception ex) {
                //Logger.LogError($"Error occured at MonitoringRequestService.RequestUrlAndSaveDb(Monitor monitor), {ex}");
            }
        }

        private void UpdateMonitoringLastStatusAndLastCheckDate(Monitor monitor, bool isSuccess) {
            var result = MonitoringService.GetById(monitor.Id);

            result.LastCheckDate = DateTime.Now;
            result.LastStatus = isSuccess ? MonitorStatus.Success : MonitorStatus.Fail;
            MonitoringService.Edit(result);
            MonitoringService.Save();
        }

    }
}
