namespace Wingman.Tool.Generation
{
    using Castle.MicroKernel;

    using Wingman.Tool.Cmd;
    using Wingman.Tool.DI;

    public class ProjectGeneratorFactory : IProjectGeneratorFactory
    {
        private readonly IBootstrapper _bootstrapper;

        public ProjectGeneratorFactory(IBootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        public IProjectGenerator CreateGeneratorFor(ProjectType projectType)
        {
            return _bootstrapper.Resolve<IProjectGenerator>(new Arguments
            {
                [nameof(ProjectType)] = projectType
            });
        }
    }
}