namespace Wingman.Tool.DI
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using NLog;

    using Wingman.Tool.Api;
    using Wingman.Tool.Generation;
    using Wingman.Tool.Handlers;

    public class DependenciesInstaller : IWindsorInstaller
    {
        private readonly IBootstrapper _bootstrapper;

        private readonly ILogger _logger;

        public DependenciesInstaller(IBootstrapper bootstrapper, ILogger logger)
        {
            _bootstrapper = bootstrapper;
            _logger = logger;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBootstrapper>()
                                        .Instance(_bootstrapper),
                               Component.For<ILogger>()
                                        .Instance(_logger),
                               Component.For<ICreateHandler>()
                                        .ImplementedBy<CreateHandler>(),
                               Component.For<IErrorHandler>()
                                        .ImplementedBy<ErrorHandler>(),
                               Component.For<IProjectGeneratorFactory>()
                                        .ImplementedBy<ProjectGeneratorFactory>(),
                               Component.For<IProjectGenerator>()
                                        .ImplementedBy<ProjectGenerator>(),
                               Component.For<IDirectoryManipulator>()
                                        .ImplementedBy<DirectoryManipulator>(),
                               Component.For<IFileManipulator>()
                                        .ImplementedBy<FileManipulator>(),
                               Component.For<IProjectDirectoryProvider, ProjectDirectoryProvider>()
                                        .ImplementedBy<ProjectDirectoryProvider>()
                                        .LifestyleSingleton(),
                               Component.For<ISupportedSolutionTemplates, ISolutionTemplateProvider>()
                                        .ImplementedBy<SolutionTemplateProvider>(),
                               Component.For<IToolApiClient>()
                                        .ImplementedBy<ToolApiClient>(),
                               Component.For<IGitClient>()
                                        .ImplementedBy<GitClient>(),
                               Component.For<ICommandLineExecutor>()
                                        .ImplementedBy<CommandLineExecutor>());
        }
    }
}