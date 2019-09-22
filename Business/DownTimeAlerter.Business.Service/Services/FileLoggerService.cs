using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DownTimeAlerter.Business.Service.Services {
    public class FileLoggerService : ILogger {
        public string Path { get; }

        public FileLoggerService(string Path) {
            this.Path = Path;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
            var message = string.Format("{0}: {1} - {2}", logLevel.ToString(), eventId.Id, formatter(state, exception));
            WriteMessageToFile(message);
        }
        private void WriteMessageToFile(string message) {
            using (var streamWriter = new StreamWriter(Path, true)) {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
        public IDisposable BeginScope<TState>(TState state) {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel) {
            return true;
        }
    }
}
