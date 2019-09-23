namespace DownTimeAlerter.Business.Service.IServices {
    /// <summary>
    /// Hangfire Service for Job Creation
    /// </summary>
    public interface IHangfireService {
        /// <summary>
        /// Create Monitoring Jobs
        /// </summary>
        void CreateRecurringJobsForMonitorings();
    }
}
