namespace Wingman.Tool.Generation
{
    public class GitClient : IGitClient
    {
        private readonly IProjectDirectoryProvider _projectDirectoryProvider;

        private readonly ICommandLineExecutor _commandLineExecutor;

        public GitClient(IProjectDirectoryProvider projectDirectoryProvider, ICommandLineExecutor commandLineExecutor)
        {
            _projectDirectoryProvider = projectDirectoryProvider;
            _commandLineExecutor = commandLineExecutor;
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
            _commandLineExecutor.ExecuteCommandInDirectoryWithArguments("git", _projectDirectoryProvider.SolutionDirectory, arguments);
        }
    }
}