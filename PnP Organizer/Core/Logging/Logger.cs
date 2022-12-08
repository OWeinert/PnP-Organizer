using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PnP_Organizer.Logging
{
    internal class Logger
    {
        private static string s_logFilePath = string.Empty;
        private static string s_logDirectoryPath = string.Empty;

        private static bool s_logEnabled = false;

        public static void StartLogging(string logDirectoryPath = "")
        {
            s_logDirectoryPath = logDirectoryPath == string.Empty ? $"{Directory.GetCurrentDirectory()}\\logs" : logDirectoryPath;
            System.Diagnostics.Debug.AutoFlush = true;
            CreateLogFile();
            s_logEnabled = true; 
            Log("Beginning to log...");
        }

        public static void EndLogging()
        {
            s_logEnabled = false;
            Log("End of Log");
        }

        public static void Log(object? message) => WriteLogMessage(LogLevel.INFO, message);

        public static void LogWarning(object? message) => WriteLogMessage(LogLevel.WARN, message);

        public static void LogError(object? message) => WriteLogMessage(LogLevel.ERR, message);

        public static void LogFatal(object? message) => WriteLogMessage(LogLevel.FATAL, message);

        public static void LogDebug(object? message) => WriteLogMessage(LogLevel.DEBUG, message);

        public static void LogException(Exception e, bool silent = false, string message = "")
        {
            StackTrace? stackTrace = new(e, true);
            var logLevel = silent ? LogLevel.WARN : LogLevel.ERR;
            var silentPrefix = silent ? "Silently caught" : "Caught";
            var customMessage = message != "" ? $"{message}\n" : "";
            WriteLogMessage(logLevel,
                $"{customMessage}" +
                $"{silentPrefix} Exception:\n" +
                $"{e.GetType()}: {e.Message}\n" +
                $"{e.StackTrace}",
                stackTrace, stackTrace.FrameCount - 1);
        }

        private static void WriteLogMessage(LogLevel logLevel, object? message, StackTrace? stackTrace = null, int stackFrameLevel = 2)
        {
            if (s_logEnabled)
            {
                var messageStr = message?.ToString();
                stackTrace ??= new(true);
                var stackFrame = stackTrace?.GetFrame(stackFrameLevel);
                var fileName = Path.GetFileName(stackFrame?.GetFileName());
                var logOutput = $"[{DateTime.Now:HH:mm:ss}] [{fileName}/{logLevel}]: {messageStr}";
                System.Diagnostics.Debug.WriteLine(logOutput);
                WriteToLogFile($"{logOutput}\n");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"[{DateTime.Now:HH:mm:ss}] [Logger.cs/ERR]: Cannot write to log, because logging is disabled!\n" +
                    $"Enable the Logger first by calling Logger.StartLogging()!");
            }
        }

        private static void WriteToLogFile(string log) => File.AppendAllText(s_logFilePath, log);

        private static void CreateLogFile()
        {
            if (!Directory.Exists(s_logDirectoryPath))
                Directory.CreateDirectory(s_logDirectoryPath);
            s_logFilePath = $"{s_logDirectoryPath}\\log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
            using var fs = File.Create(s_logFilePath);
            fs.Close();
        }

        public static void DeleteOldLogFiles(int maxLogFiles)
        {
            IEnumerable<FileInfo> oldLogs = new DirectoryInfo(s_logDirectoryPath).GetFiles().OrderByDescending(x => x.LastWriteTime);
            if (oldLogs.Count() > maxLogFiles)
            {
                System.Diagnostics.Debug.WriteLine($"More than allowed number of log files ({maxLogFiles}). Deleting the oldest log file...");
                oldLogs.Last().Delete();
            }
        }

        private enum LogLevel
        {
            DEBUG,
            INFO,
            WARN,
            ERR,
            FATAL
        }
    }
}
