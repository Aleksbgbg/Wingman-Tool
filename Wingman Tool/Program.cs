namespace Wingman.Tool
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using CommandLine;

    using Wingman.Tool.Cmd;
    using Wingman.Tool.DI;
    using Wingman.Tool.Generation;

    internal static class Program
    {
        private static IProjectGeneratorFactory projectGeneratorFactory;

        private static ProjectDirectoryProvider projectDirectoryProvider;

        private static int Main(string[] args)
        {
            IBootstrapper bootstrapper = Bootstrapper.BootstrapDependencies();

            projectGeneratorFactory = bootstrapper.Resolve<IProjectGeneratorFactory>();
            projectDirectoryProvider = bootstrapper.Resolve<ProjectDirectoryProvider>();

            return Parser.Default
                         .ParseArguments<CreateOptions>(args)
                         .MapResult(RunCreateAndReturnExitCode,
                                    HandleErrors);
        }

        private static int RunCreateAndReturnExitCode(CreateOptions options)
        {
            if (options.UnitTest)
            {
                options.ProjectType += "UnitTest";
            }

            if (!projectGeneratorFactory.SupportsProjectType(options.ProjectType))
            {
                Console.WriteLine("Project type not supported.");
                return -1;
            }

            projectDirectoryProvider.SolutionDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.Name);

            IProjectGenerator projectGenerator = projectGeneratorFactory.CreateGeneratorFor(options.ProjectType);

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

        private static int HandleErrors(IEnumerable<Error> errors)
        {
            return -1;
        }
    }
}