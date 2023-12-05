using Microsoft.AspNetCore.Mvc.RazorPages;
using Reviver.Logging;
using System.Text;

namespace Reviver.Pages
{
    public class IndexModel : PageModel
    {
        private const string LogsAccessKey = "Q85AKdeSQ8Wsz1c7";

        public bool HasPermission => string.IsNullOrWhiteSpace(PermissionInfo);

        public string PermissionInfo { get; set; }

        public List<List<string>> TraceLogs { get; } = new();

        public List<List<string>> DebugLogs { get; } = new();

        public List<List<string>> InfoLogs { get; } = new();

        public List<List<string>> WarnLogs { get; } = new();

        public List<List<string>> ErrorLogs { get; } = new();

        public List<List<string>> CriticalLogs { get; } = new();

        public List<List<string>> NoneLogs { get; } = new();


        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet(string accessKey)
        {
            if (accessKey == LogsAccessKey)
            {
                var logFilePath = _configuration.GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>().LogFilePath;
                var logDirectory = Path.GetDirectoryName(logFilePath);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(logFilePath);
                var fileExtension = Path.GetExtension(logFilePath);
                var logFiles = Directory.GetFiles(logDirectory, $"{fileNameWithoutExtension}*{fileExtension}")
                    .OrderByDescending(System.IO.File.GetLastWriteTime).Reverse();

                foreach (var logFile in logFiles)
                {
                    using var fs = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using var sr = new StreamReader(fs, Encoding.Default);
                    var buffer = new char[(int)sr.BaseStream.Length];
                    sr.Read(buffer, 0, (int)sr.BaseStream.Length);

                    var rawLogs = new string(buffer);
                    var stringSeparators = new[] { "\r\n" };
                    var lines = rawLogs.Split(stringSeparators, StringSplitOptions.None);

                    var log = new List<string>();
                    foreach (var line in lines)
                    {
                        var isNewLog = false;

                        if (line.StartsWith("TRACE"))
                        {
                            isNewLog = true;

                            log = new List<string> { line };
                            TraceLogs.Add(log);
                        }

                        if (line.StartsWith("DEBUG"))
                        {
                            isNewLog = true;

                            log = new List<string> { line };
                            DebugLogs.Add(log);
                        }

                        if (line.StartsWith("INFORMATION"))
                        {
                            isNewLog = true;

                            log = new List<string> { line };
                            InfoLogs.Add(log);
                        }

                        if (line.StartsWith("WARN"))
                        {
                            isNewLog = true;

                            log = new List<string> { line };
                            WarnLogs.Add(log);
                        }

                        if (line.StartsWith("ERROR"))
                        {
                            isNewLog = true;

                            log = new List<string> { line };
                            ErrorLogs.Add(log);
                        }

                        if (line.StartsWith("CRITICAL"))
                        {
                            isNewLog = true;

                            log = new List<string> { line };
                            CriticalLogs.Add(log);
                        }

                        if (line.StartsWith("NONE"))
                        {
                            isNewLog = true;

                            log = new List<string> { line };
                            NoneLogs.Add(log);
                        }

                        if (!isNewLog)
                            log.Add(line);
                    }
                }

                TraceLogs.Reverse();
                DebugLogs.Reverse();
                InfoLogs.Reverse();
                WarnLogs.Reverse();
                ErrorLogs.Reverse();
                CriticalLogs.Reverse();
                NoneLogs.Reverse();
            }
            else
            {
                PermissionInfo = "Brak uprawnień!";
            }
        }
    }
}