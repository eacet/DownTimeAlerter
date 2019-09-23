using DownTimeAlerter.Data.Domain.Models;

namespace DownTimeAlerter.Business.Service.IServices {
    /// <summary>
    /// Notification Service
    /// </summary>
    public interface INotificationService {
        /// <summary>
        /// Send Notification
        /// </summary>
        /// <param name="notificationModel"></param>
        void Notify(NotificationModel notificationModel);
    }
}
