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

            SetupNlog();

            container.Install(new DependenciesInstaller(bootstrapper));

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

        private static void SetupNlog()
        {
            LoggingConfiguration config = new LoggingConfiguration();
            ConsoleTarget consoleTarget = new ConsoleTarget();

            config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget);

            LogManager.Configuration = config;
        }
    }
}