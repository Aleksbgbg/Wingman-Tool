namespace Wingman.Tool
{
    using System.Collections.Generic;

    using CommandLine;

    using Wingman.Tool.Cmd;
    using Wingman.Tool.DI;
    using Wingman.Tool.Generation;

    internal static class Program
    {
        private static IProjectGeneratorFactory _projectGeneratorFactory;

        private static int Main(string[] args)
        {
            _projectGeneratorFactory = Bootstrapper.BootstrapDependencies().Resolve<IProjectGeneratorFactory>();

            return new Parser(settings => settings.CaseInsensitiveEnumValues = true)
                         .ParseArguments<CreateOptions>(args)
                         .MapResult(RunCreateAndReturnExitCode,
                                    HandleErrors);
        }

        private static int RunCreateAndReturnExitCode(CreateOptions options)
        {
            return 0;
        }

        private static int HandleErrors(IEnumerable<Error> errors)
        {
            return 1;
        }
    }
}