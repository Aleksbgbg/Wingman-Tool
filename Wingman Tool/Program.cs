namespace Wingman.Tool
{
    using System.Collections.Generic;

    using CommandLine;

    using Wingman.Tool.Cmd;
    using Wingman.Tool.DI;
    using Wingman.Tool.Generation;

    internal static class Program
    {
        private static int Main(string[] args)
        {
            IBootstrapper bootstrapper = Bootstrapper.BootstrapDependencies();

            return new Parser(settings => settings.CaseInsensitiveEnumValues = true)
                         .ParseArguments<CreateOptions>(args)
                         .MapResult(options => RunCreateAndReturnExitCode(options, bootstrapper.Resolve<IProjectGeneratorFactory>()),
                                    HandleErrors);
        }

        private static int RunCreateAndReturnExitCode(CreateOptions options, IProjectGeneratorFactory projectGeneratorFactory)
        {
            return 0;
        }

        private static int HandleErrors(IEnumerable<Error> errors)
        {
            return 1;
        }
    }
}