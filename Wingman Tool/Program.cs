﻿namespace Wingman.Tool
{
    using System.Collections.Generic;

    using CommandLine;

    using Wingman.Tool.Cmd;
    using Wingman.Tool.DI;
    using Wingman.Tool.Generation;

    internal static class Program
    {
        private static IProjectGeneratorFactory projectGeneratorFactory;

        private static int Main(string[] args)
        {
            projectGeneratorFactory = Bootstrapper.BootstrapDependencies().Resolve<IProjectGeneratorFactory>();

            return new Parser(settings => settings.CaseInsensitiveEnumValues = true)
                         .ParseArguments<CreateOptions>(args)
                         .MapResult(RunCreateAndReturnExitCode,
                                    HandleErrors);
        }

        private static int RunCreateAndReturnExitCode(CreateOptions options)
        {
            IProjectGenerator projectGenerator = projectGeneratorFactory.CreateGeneratorFor(options.ProjectType);

            projectGenerator.GenerateProject(options.Name);

            if (options.UseGit)
            {
                projectGenerator.InitGit();

                if (options.ReadmeDescription != null)
                {
                    projectGenerator.AddReadme(options.ReadmeDescription);
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
            return 1;
        }
    }
}