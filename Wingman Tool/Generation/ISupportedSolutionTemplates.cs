namespace Wingman.Tool.Generation
{
    using System.Threading.Tasks;

    public interface ISupportedSolutionTemplates
    {
        Task<bool> IsSupported(string projectType);
    }
}