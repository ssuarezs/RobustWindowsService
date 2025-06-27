using System;
using System.IO;
using System.Text;
using RobustWindowsService.Application;

namespace RobustWindowsService.Infrastructure.Logging
{
    public class TxtLoggingService : ILoggingService
    {
        private readonly string _logFilePath;
        // Objeto para bloquear y asegurar que la escritura al archivo sea segura entre hilos (thread-safe)
        private static readonly object _lock = new object();

        public TxtLoggingService()
        {
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(logDirectory); // Asegura que la carpeta exista
            _logFilePath = Path.Combine(logDirectory, $"service_{DateTime.Now:yyyyMMdd}.log");
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }

        public void LogError(string message, Exception ex = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine(message);
            if (ex != null)
            {
                sb.AppendLine(ex.ToString());
            }
            WriteLog("ERROR", sb.ToString());
        }

        private void WriteLog(string level, string message)
        {
            // Usamos 'lock' para prevenir que dos hilos escriban en el archivo al mismo tiempo
            lock (_lock)
            {
                var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}{Environment.NewLine}";
                File.AppendAllText(_logFilePath, logEntry);
            }
        }
    }
}
