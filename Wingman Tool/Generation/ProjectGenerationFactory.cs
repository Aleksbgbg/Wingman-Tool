namespace Wingman.Tool.Generation
{
    using System;

    using Wingman.Tool.Cmd;
    using Wingman.Tool.DI;
    using Wingman.Tool.Generation.Wpf;

    public class ProjectGeneratorFactory : IProjectGeneratorFactory
    {
        private readonly IBootstrapper _bootstrapper;

        public ProjectGeneratorFactory(IBootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        public IProjectGenerator CreateGeneratorFor(ProjectType projectType)
        {
            switch (projectType)
            {
                case ProjectType.Wpf:
                    return _bootstrapper.Resolve<WpfProjectGenerator>();

                default:
                    throw new ArgumentOutOfRangeException(nameof(projectType), projectType, "Project type not implemented.");
            }
        }
    }
}