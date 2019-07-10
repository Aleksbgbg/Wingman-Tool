namespace Wingman.Tool.DI
{
    using Castle.Windsor;
    using Castle.Windsor.Installer;

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

            container.Install(FromAssembly.This());

            return new Bootstrapper(container);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}