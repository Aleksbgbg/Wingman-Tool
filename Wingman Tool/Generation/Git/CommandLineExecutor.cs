namespace Wingman.Tool.Generation.Git
{
    using System.Diagnostics;
    using System.IO;

    using NLog;

    public class CommandLineExecutor : ICommandLineExecutor
    {
        private readonly ILogger _logger;

        public CommandLineExecutor(ILogger logger)
        {
            _logger = logger;
        }

        public void ExecuteCommandInDirectoryWithArguments(string command, string directory, string arguments)
        {
            Process process = Process.Start(new ProcessStartInfo(command, arguments)
            {
                WorkingDirectory = directory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            });

            process.WaitForExit();

            using (process.StandardOutput)
            {
                ReadStreamToInfoLogger(process.StandardOutput, command);
            }

            using (process.StandardError)
            {
                ReadStreamToWarnLogger(process.StandardError, command);
            }
        }

        private void ReadStreamToInfoLogger(StreamReader streamReader, string processName)
        {
            ReadStreamToLogger(LogLevel.Info, streamReader, processName);
        }

        private void ReadStreamToWarnLogger(StreamReader streamReader, string processName)
        {
            ReadStreamToLogger(LogLevel.Warn, streamReader, processName);
        }

        private void ReadStreamToLogger(LogLevel logLevel, StreamReader streamReader, string processName)
        {
            foreach (string line in streamReader.ReadToEnd().Split('\n'))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    _logger.Log(logLevel, $"({processName}) {line}");
                }
            }
        }
    }
}