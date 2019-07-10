namespace Wingman.Tool.Generation
{
    using System;
    using System.IO;

    using Wingman.Tool.Cmd;

    public class CreateHandler : ICreateHandler
    {
        private readonly IProjectGeneratorFactory _projectGeneratorFactory;

        private readonly ProjectDirectoryProvider _projectDirectoryProvider;

        public CreateHandler(IProjectGeneratorFactory projectGeneratorFactory, ProjectDirectoryProvider projectDirectoryProvider)
        {
            _projectGeneratorFactory = projectGeneratorFactory;
            _projectDirectoryProvider = projectDirectoryProvider;
        }

        public int HandleAndReturnExitCode(CreateOptions options)
        {
            if (options.UnitTest)
            {
                options.ProjectType += "UnitTest";
            }

            if (!_projectGeneratorFactory.SupportsProjectType(options.ProjectType))
            {
                Console.WriteLine("Project type not supported.");
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