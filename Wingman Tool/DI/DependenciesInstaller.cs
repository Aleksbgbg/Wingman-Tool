namespace Wingman.Tool.DI
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Wingman.Tool.Generation;

    public class DependenciesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IProjectGeneratorFactory>().ImplementedBy<ProjectGeneratorFactory>());
        }
    }
}