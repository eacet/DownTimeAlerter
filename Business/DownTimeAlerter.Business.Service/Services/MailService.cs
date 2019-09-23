using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.Domain.Models;
using System;
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
            try {
                SmtpClient client = new SmtpClient {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587
                };

                var credentials = new NetworkCredential("erhanacetproj@gmail.com", "Erhan!123");
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                var mailMessage = CreateMailMessage(notificationModel);

                client.Send(mailMessage);
            }
            catch (Exception ex) {

            }
        }

        /// <summary>
        /// Create Mail Message for Mail Notification
        /// </summary>
        /// <param name="notificationModel"></param>
        /// <returns></returns>
        private MailMessage CreateMailMessage(NotificationModel notificationModel) {
            var mailMessage = new MailMessage { From = new MailAddress("erhanacetproj@gmail.com", notificationModel.DisplayName) };

            mailMessage.To.Add(notificationModel.NotificationUserModel.Mail);
            mailMessage.Subject = notificationModel.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = notificationModel.Message;
            return mailMessage;
        }
    }
}
