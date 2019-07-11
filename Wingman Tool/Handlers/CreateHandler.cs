namespace Wingman.Tool.Handlers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using NLog;

    using Wingman.Tool.Generation;
    using Wingman.Tool.Generation.FileIO;
    using Wingman.Tool.Handlers.Options;

    public class CreateHandler : ICreateHandler
    {
        private readonly IProjectGeneratorFactory _projectGeneratorFactory;

        private readonly ProjectDirectoryProvider _projectDirectoryProvider;

        private readonly ILogger _logger;

        private readonly IDirectoryManipulator _directoryManipulator;

        public CreateHandler(IProjectGeneratorFactory projectGeneratorFactory, ProjectDirectoryProvider projectDirectoryProvider, ILogger logger, IDirectoryManipulator directoryManipulator)
        {
            _projectGeneratorFactory = projectGeneratorFactory;
            _projectDirectoryProvider = projectDirectoryProvider;
            _logger = logger;
            _directoryManipulator = directoryManipulator;
        }

        public int HandleAndReturnExitCode(CreateOptions options)
        {
            return HandleAndReturnExitCodeAsync(options).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async Task<int> HandleAndReturnExitCodeAsync(CreateOptions options)
        {
            if (options.UnitTest)
            {
                options.ProjectType += "UnitTest";
            }

            if (!(await _projectGeneratorFactory.SupportsProjectType(options.ProjectType)))
            {
                _logger.Warn("Project type {ProjectType} not supported.", options.ProjectType);
                return -1;
            }

            _projectDirectoryProvider.SolutionDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.Name);
            _logger.Info($"Generating solution directory \"{options.Name}\".");
            _directoryManipulator.CreateDirectory(_projectDirectoryProvider.SolutionDirectory);

            IProjectGenerator projectGenerator = _projectGeneratorFactory.CreateGeneratorFor(options.ProjectType);

            try
            {
                await projectGenerator.GenerateProject(options.Name);

                if (options.InitGit)
                {
                    projectGenerator.InitGit();

                    if (options.UseGitMetadata)
                    {
                        await projectGenerator.AddGitMetadata();
                    }

                    if (options.ReadmeDescription != null)
                    {
                        projectGenerator.AddReadme(options.Name, options.ReadmeDescription);
                    }

                    if (options.CommitMessage != null)
                    {
                        projectGenerator.Commit(options.CommitMessage);
                    }

                    if (options.GitRemote != null)
                    {
                        projectGenerator.AddRemote(options.GitRemote);

                        if (options.Push)
                        {
                            projectGenerator.Push();
                        }
                    }
                }
            }
            catch (Exception)
            {
                _logger.Error("An error ocurred during project generation.");
            }

            return 0;
        }
    }
}