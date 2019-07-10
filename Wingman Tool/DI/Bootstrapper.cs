namespace Wingman.Tool.DI
{
    using Castle.MicroKernel;
    using Castle.Windsor;

    using NLog;
    using NLog.Config;
    using NLog.Targets;

    public class Bootstrapper : IBootstrapper
    {
        private readonly WindsorContainer _container;

        private Bootstrapper(WindsorContainer container)
        {
            _container = container;
        }

        public static IBootstrapper BootstrapApplication()
        {
            WindsorContainer container = new WindsorContainer();

            Bootstrapper bootstrapper = new Bootstrapper(container);
            ILogger logger = SetupNlog();

            container.Install(new DependenciesInstaller(bootstrapper, logger));

            return bootstrapper;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(Arguments arguments)
        {
            return _container.Resolve<T>(arguments);
        }

        private static ILogger SetupNlog()
        {
            LoggingConfiguration config = new LoggingConfiguration();
            ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level:padding=5:uppercase=true} ${message}"
            };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget);

            LogManager.Configuration = config;

            return LogManager.LogFactory.GetLogger("Main");
        }
    }
}