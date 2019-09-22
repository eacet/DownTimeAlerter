using DownTimeAlerter.Business.Service.Model;

namespace DownTimeAlerter.Business.Service.IServices {
    public interface INotificationService {
        void Notify(NotificationModel notificationModel);
    }
}
