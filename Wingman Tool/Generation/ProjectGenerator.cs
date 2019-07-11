namespace Wingman.Tool.Generation
{
    using System;
    using System.Threading.Tasks;

    using NLog;

    public class ProjectGenerator : IProjectGenerator
    {
        private readonly IDirectoryManipulator _directoryManipulator;

        private readonly IFileManipulator _fileManipulator;

        private readonly ISolutionTemplateProvider _solutionTemplateProvider;

        private readonly IGitClient _gitClient;

        private readonly ILogger _logger;

        private readonly string _projectType;

        private readonly string _solutionDirectory;

        public ProjectGenerator(IProjectDirectoryProvider projectDirectoryProvider,
                                IDirectoryManipulator directoryManipulator,
                                IFileManipulator fileManipulator,
                                ISolutionTemplateProvider solutionTemplateProvider,
                                IGitClient gitClient,
                                ILogger logger,
                                string projectType
        )
        {
            _directoryManipulator = directoryManipulator;
            _fileManipulator = fileManipulator;
            _solutionTemplateProvider = solutionTemplateProvider;
            _gitClient = gitClient;
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

        public void AddReadme(string projectName, string description)
        {
            const string readmeRelativePath = "README.md";
            string readmePath = _directoryManipulator.PathNameRelativeToDirectory(_solutionDirectory, readmeRelativePath);
            string readmeContents = $"# {projectName}{Environment.NewLine}{description}{Environment.NewLine}";

            LogFile(readmeRelativePath);
            _logger.Info($"README contents:\n{readmeContents}");
            _fileManipulator.CreateFile(readmePath, readmeContents);
        }

        public void AddRemote(string url)
        {
            _logger.Info("Adding git remote (origin) at {Url}.", url);
            _gitClient.AddRemote(url);
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