namespace Wingman.Tool.Generation
{
    using System;
    using System.Threading.Tasks;

    public class ProjectGenerator : IProjectGenerator
    {
        private readonly IDirectoryManipulator _directoryManipulator;

        private readonly IFileManipulator _fileManipulator;

        private readonly ISolutionTemplateProvider _solutionTemplateProvider;

        private readonly IGitClient _gitClient;

        private readonly string _projectType;

        private readonly string _solutionDirectory;

        public ProjectGenerator(IProjectDirectoryProvider projectDirectoryProvider,
                                IDirectoryManipulator directoryManipulator,
                                IFileManipulator fileManipulator,
                                ISolutionTemplateProvider solutionTemplateProvider,
                                IGitClient gitClient,
                                string projectType
        )
        {
            _directoryManipulator = directoryManipulator;
            _fileManipulator = fileManipulator;
            _solutionTemplateProvider = solutionTemplateProvider;
            _gitClient = gitClient;
            _projectType = projectType;
            _solutionDirectory = projectDirectoryProvider.SolutionDirectory;
        }

        public async Task GenerateProject(string projectName)
        {
            FileTreeTemplate fileTreeTemplate = await _solutionTemplateProvider.TemplateFor(_projectType);

            foreach (FileTreeEntry fileTreeEntry in fileTreeTemplate.Entries)
            {
                RenderedFileTreeEntry renderedEntry = await _solutionTemplateProvider.RenderFileTreeEntry(_projectType, projectName, fileTreeEntry);

                string path = _directoryManipulator.PathNameRelativeToDirectory(_solutionDirectory, renderedEntry.RelativePath);

                if (renderedEntry.IsDirectory)
                {
                    _directoryManipulator.CreateDirectory(path);
                }
                else
                {
                    _fileManipulator.CreateFile(path, renderedEntry.Contents);
                }
            }
        }

        public void InitGit()
        {
            _gitClient.Init();
        }

        public void AddReadme(string projectName, string description)
        {
            string readmePath = _directoryManipulator.PathNameRelativeToDirectory(_solutionDirectory, "README.md");
            string readmeContents = $"# {projectName}{Environment.NewLine}{description}{Environment.NewLine}";

            _fileManipulator.CreateFile(readmePath, readmeContents);
        }

        public void AddRemote(string url)
        {
            _gitClient.AddRemote(url);
        }
    }
}