namespace Wingman.Tool.Generation
{
    public interface IProjectGeneratorFactory
    {
        bool SupportsProjectType(string projectType);

        IProjectGenerator CreateGeneratorFor(string projectType);
    }
}