using Microsoft.Extensions.Logging;

namespace DownTimeAlerter.Business.Service.Services {
    public class FileLoggerProvider : ILoggerProvider {
        public FileLoggerProvider(string path) {
            Path = path;
        }

        public string Path { get; }

        public ILogger CreateLogger(string categoryName) {
            return new FileLoggerService(Path);
        }

        public void Dispose() {

        }
    }
}
