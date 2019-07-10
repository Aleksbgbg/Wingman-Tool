namespace Wingman.Tool.Handlers
{
    using System;
    using System.IO;

    using NLog;

    using Wingman.Tool.Cmd;
    using Wingman.Tool.Generation;

    public class CreateHandler : ICreateHandler
    {
        private readonly IProjectGeneratorFactory _projectGeneratorFactory;

        private readonly ProjectDirectoryProvider _projectDirectoryProvider;

        private readonly ILogger _logger;

        public CreateHandler(IProjectGeneratorFactory projectGeneratorFactory, ProjectDirectoryProvider projectDirectoryProvider, ILogger logger)
        {
            _projectGeneratorFactory = projectGeneratorFactory;
            _projectDirectoryProvider = projectDirectoryProvider;
            _logger = logger;
        }

        public int HandleAndReturnExitCode(CreateOptions options)
        {
            if (options.UnitTest)
            {
                options.ProjectType += "UnitTest";
            }

            if (!_projectGeneratorFactory.SupportsProjectType(options.ProjectType))
            {
                _logger.Error("Project type not supported.");
                return -1;
            }

            _projectDirectoryProvider.SolutionDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.Name);

            IProjectGenerator projectGenerator = _projectGeneratorFactory.CreateGeneratorFor(options.ProjectType);

            projectGenerator.GenerateProject(options.Name);

            if (options.UseGit)
            {
                projectGenerator.InitGit();

                if (options.ReadmeDescription != null)
                {
                    projectGenerator.AddReadme(options.Name, options.ReadmeDescription);
                }

                if (options.GitRemote != null)
                {
                    projectGenerator.AddRemote(options.GitRemote);
                }
            }

            return 0;
        }
    }
}