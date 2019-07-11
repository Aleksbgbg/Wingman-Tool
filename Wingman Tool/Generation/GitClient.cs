namespace Wingman.Tool.Generation
{
    using System.ComponentModel;

    using NLog;

    public class GitClient : IGitClient
    {
        private readonly IProjectDirectoryProvider _projectDirectoryProvider;

        private readonly ICommandLineExecutor _commandLineExecutor;

        private readonly ILogger _logger;

        public GitClient(IProjectDirectoryProvider projectDirectoryProvider, ICommandLineExecutor commandLineExecutor, ILogger logger)
        {
            _projectDirectoryProvider = projectDirectoryProvider;
            _commandLineExecutor = commandLineExecutor;
            _logger = logger;
        }

        public void Init()
        {
            ExecuteGitCommand("init");
        }

        public void AddAll()
        {
            ExecuteGitCommand("add .");
        }

        public void Commit(string commitMessage)
        {
            ExecuteGitCommand($"commit -m \"{commitMessage}\"");
        }

        public void AddRemote(string url)
        {
            ExecuteGitCommand($"remote add origin {url}");
        }

        public void Push()
        {
            ExecuteGitCommand("push -u origin master");
        }

        private void ExecuteGitCommand(string arguments)
        {
            try
            {
                _commandLineExecutor.ExecuteCommandInDirectoryWithArguments("git", _projectDirectoryProvider.SolutionDirectory, arguments);
            }
            catch (Win32Exception)
            {
                _logger.Warn("There was a problem executing a git command. Please ensure you have git, and your git folder is added to the %PATH% environment variable.");
                throw;
            }
        }
    }
}