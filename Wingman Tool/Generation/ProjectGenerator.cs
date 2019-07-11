namespace Wingman.Tool.Generation
{
    using System;
    using System.Threading.Tasks;

    using NLog;

    using Wingman.Tool.Generation.Api;
    using Wingman.Tool.Generation.FileIO;
    using Wingman.Tool.Generation.Git;
    using Wingman.Tool.Generation.Templates;

    public class ProjectGenerator : IProjectGenerator
    {
        private readonly IDirectoryManipulator _directoryManipulator;

        private readonly IFileManipulator _fileManipulator;

        private readonly ISolutionTemplateProvider _solutionTemplateProvider;

        private readonly IGitClient _gitClient;

        private readonly IGitApiClient _gitApiClient;

        private readonly ILogger _logger;

        private readonly string _projectType;

        private readonly string _solutionDirectory;

        public ProjectGenerator(IProjectDirectoryProvider projectDirectoryProvider,
                                IDirectoryManipulator directoryManipulator,
                                IFileManipulator fileManipulator,
                                ISolutionTemplateProvider solutionTemplateProvider,
                                IGitClient gitClient,
                                IGitApiClient gitApiClient,
                                ILogger logger,
                                string projectType
        )
        {
            _directoryManipulator = directoryManipulator;
            _fileManipulator = fileManipulator;
            _solutionTemplateProvider = solutionTemplateProvider;
            _gitClient = gitClient;
            _gitApiClient = gitApiClient;
            _logger = logger;
            _projectType = projectType;
            _solutionDirectory = projectDirectoryProvider.SolutionDirectory;
        }

        public async Task GenerateProject(string projectName)
        {
            _logger.Info("Generating Visual Studio project using template for {ProjectType} project type.", _projectType);

            FileTreeTemplate fileTreeTemplate = await _solutionTemplateProvider.TemplateFor(_projectType);

            foreach (FileTreeEntry fileTreeEntry in fileTreeTemplate.Entries)
            {
                RenderedFileTreeEntry renderedEntry = await _solutionTemplateProvider.RenderFileTreeEntry(_projectType, projectName, fileTreeEntry);

                string path = _directoryManipulator.PathNameRelativeToDirectory(_solutionDirectory, renderedEntry.RelativePath);

                if (renderedEntry.IsDirectory)
                {
                    LogDirectory(renderedEntry.RelativePath);
                    _directoryManipulator.CreateDirectory(path);
                }
                else
                {
                    LogFile(renderedEntry.RelativePath);
                    _fileManipulator.CreateFile(path, renderedEntry.Contents);
                }
            }
        }

        public void InitGit()
        {
            _logger.Info("Initializing git repository.");
            _gitClient.Init();
        }

        public async Task AddGitMetadata()
        {
            AddFile(".gitattributes", await _gitApiClient.GetGitAttributes());
            AddFile(".gitignore", await _gitApiClient.GetGitIgnore());
        }

        public void AddReadme(string projectName, string description)
        {
            AddFileLogContents("README.md", $"# {projectName}{Environment.NewLine}{description}{Environment.NewLine}");
        }

        public void Commit(string commitMessage)
        {
            _logger.Info("Adding repository files to staging area.");
            _gitClient.AddAll();

            _logger.Info("Committing files in staging area with message {CommitMessage}.", commitMessage);
            _gitClient.Commit(commitMessage);
        }

        public void AddRemote(string url)
        {
            _logger.Info("Adding git remote (origin) at {Url}.", url);
            _gitClient.AddRemote(url);
        }

        public void Push()
        {
            _logger.Info("Pushing commits to origin remote, master branch.");
            _gitClient.Push();
        }

        private void AddFile(string relativePath, string contents)
        {
            string fullPath = _directoryManipulator.PathNameRelativeToDirectory(_solutionDirectory, relativePath);

            LogFile(relativePath);
            _fileManipulator.CreateFile(fullPath, contents);
        }

        private void AddFileLogContents(string relativePath, string contents)
        {
            string fullPath = _directoryManipulator.PathNameRelativeToDirectory(_solutionDirectory, relativePath);

            LogFile(relativePath);
            _logger.Info($"{{RelativePath}} contents:\n{contents}", relativePath);
            _fileManipulator.CreateFile(fullPath, contents);
        }

        private void LogFile(string relativePath)
        {
            Log("FILE", relativePath);
        }

        private void LogDirectory(string relativePath)
        {
            Log("DIR ", relativePath);
        }

        private void Log(string type, string relativePath)
        {
            _logger.Info($"Create {type} {{RelativePath}}", relativePath);
        }
    }
}