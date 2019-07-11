namespace Wingman.Tool.Generation
{
    using System.Threading.Tasks;

    public interface IProjectGeneratorFactory
    {
        Task<bool> SupportsProjectType(string projectType);

        IProjectGenerator CreateGeneratorFor(string projectType);
    }
}