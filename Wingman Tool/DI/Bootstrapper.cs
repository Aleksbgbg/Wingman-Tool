namespace Wingman.Tool.DI
{
    using Castle.Windsor;

    public class Bootstrapper : IBootstrapper
    {
        private readonly WindsorContainer _container;

        private Bootstrapper(WindsorContainer container)
        {
            _container = container;
        }

        public static IBootstrapper BootstrapDependencies()
        {
            WindsorContainer container = new WindsorContainer();
            Bootstrapper bootstrapper = new Bootstrapper(container);

            container.Install(new DependenciesInstaller(bootstrapper));

            return bootstrapper;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}