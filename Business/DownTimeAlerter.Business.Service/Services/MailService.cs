using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Business.Service.Model;
using System.Net;
using System.Net.Mail;

namespace DownTimeAlerter.Business.Service.Services {

    /// <summary>
    /// Mail Service 
    /// </summary>
    public class MailService : INotificationService {

        /// <summary>
        /// Send Mail Notification
        /// </summary>
        /// <param name="notificationModel"></param>
        public void Notify(NotificationModel notificationModel) {
            SmtpClient client = new SmtpClient {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587
            };

            var credentials = new NetworkCredential("te8453044@gmail.com", "Abcd!123"); 
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            var mailMessage = CreateMailMessage(notificationModel);

            client.Send(mailMessage);
        }

        /// <summary>
        /// Create Mail Message for Mail Notification
        /// </summary>
        /// <param name="notificationModel"></param>
        /// <returns></returns>
        private MailMessage CreateMailMessage(NotificationModel notificationModel) {
            var mailMessage = new MailMessage { From = new MailAddress("te8453044@gmail.com", notificationModel.DisplayName) };

            mailMessage.To.Add(notificationModel.NotificationUserModel.Mail);
            mailMessage.Subject = notificationModel.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = notificationModel.Message;
            return mailMessage;
        }
    }
}
