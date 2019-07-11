namespace Wingman.Tool.Generation
{
    using System.Threading.Tasks;

    using Castle.MicroKernel;

    using Wingman.Tool.DI;
    using Wingman.Tool.Generation.Templates;

    public class ProjectGeneratorFactory : IProjectGeneratorFactory
    {
        private readonly IBootstrapper _bootstrapper;

        private readonly ISupportedSolutionTemplates _supportedSolutionTemplates;

        public ProjectGeneratorFactory(IBootstrapper bootstrapper, ISupportedSolutionTemplates supportedSolutionTemplates)
        {
            _bootstrapper = bootstrapper;
            _supportedSolutionTemplates = supportedSolutionTemplates;
        }

        public Task<bool> SupportsProjectType(string projectType)
        {
            return _supportedSolutionTemplates.IsSupported(projectType);
        }

        public IProjectGenerator CreateGeneratorFor(string projectType)
        {
            return _bootstrapper.Resolve<IProjectGenerator>(new Arguments
            {
                [nameof(projectType)] = projectType
            });
        }
    }
}