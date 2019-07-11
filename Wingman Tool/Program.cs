namespace Wingman.Tool
{
    using CommandLine;

    using NLog;

    using Wingman.Tool.DI;
    using Wingman.Tool.Handlers;
    using Wingman.Tool.Handlers.Options;

    internal static class Program
    {
        private static int Main(string[] args)
        {
            IBootstrapper bootstrapper = Bootstrapper.BootstrapApplication();

            ICreateHandler createHandler = bootstrapper.Resolve<ICreateHandler>();
            IErrorHandler errorHandler = bootstrapper.Resolve<IErrorHandler>();

            int exitCode = Parser.Default
                                 .ParseArguments<CreateOptions>(args)
                                 .MapResult(createHandler.HandleAndReturnExitCode,
                                            errorHandler.HandleAndReturnExitCode);

            ILogger logger = bootstrapper.Resolve<ILogger>();
            logger.Factory.Flush();

            return exitCode;
        }
    }
}