namespace Wingman.Tool
{
    using System;
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

            return Parser.Default
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
            Console.WriteLine("There was an error.");
            return 1;
        }
    }
}