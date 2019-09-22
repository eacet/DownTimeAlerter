namespace DownTimeAlerter.Business.Service.Model {
    public class NotificationModel {
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public NotificationUserModel NotificationUserModel { get; set; }
    }

    public class NotificationUserModel {
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
