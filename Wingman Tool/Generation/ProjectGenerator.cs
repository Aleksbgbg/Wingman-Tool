namespace Wingman.Tool.Generation
{
    using Wingman.Tool.Cmd;

    public class ProjectGenerator : IProjectGenerator
    {
        private readonly IDirectoryManipulator _directoryManipulator;

        private readonly IFileManipulator _fileManipulator;

        private readonly ISolutionTemplateProvider _solutionTemplateProvider;

        private readonly IGitClient _gitClient;

        private readonly ProjectType _projectType;

        private readonly string _currentDirectory;

        public ProjectGenerator(IProjectDirectoryProvider projectDirectoryProvider,
                                IDirectoryManipulator directoryManipulator,
                                IFileManipulator fileManipulator,
                                ISolutionTemplateProvider solutionTemplateProvider,
                                IGitClient gitClient,
                                ProjectType projectType
        )
        {
            _directoryManipulator = directoryManipulator;
            _fileManipulator = fileManipulator;
            _solutionTemplateProvider = solutionTemplateProvider;
            _gitClient = gitClient;
            _projectType = projectType;
            _currentDirectory = projectDirectoryProvider.CurrentDirectory;
        }

        public void GenerateProject(string projectName)
        {
            FileTreeTemplate fileTreeTemplate = _solutionTemplateProvider.TemplateFor(_projectType);

            foreach (FileTreeEntry entry in fileTreeTemplate.Entries)
            {
                string path = _directoryManipulator.PathNameRelativeToDirectory(_currentDirectory, entry.RelativePath);

                if (entry.IsDirectory)
                {
                    _directoryManipulator.CreateDirectory(path);
                }
                else
                {
                    string fileContents = _solutionTemplateProvider.ContentsFor(_projectType, entry);
                    _fileManipulator.CreateFile(path, fileContents);
                }
            }
        }

        public void InitGit()
        {
            _gitClient.Init();
        }

        public void AddReadme(string description)
        {
            throw new System.NotImplementedException();
        }

        public void AddRemote(string url)
        {
            _gitClient.AddRemote(url);
        }
    }
}