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

            ICreateHandler createHandler = bootstrapper.Resolve<ICreateHandler>();

            return Parser.Default
                         .ParseArguments<CreateOptions>(args)
                         .MapResult(createHandler.HandleAndReturnExitCode,
                                    HandleErrors);
        }

        private static int HandleErrors(IEnumerable<Error> errors)
        {
            return -1;
        }
    }
}