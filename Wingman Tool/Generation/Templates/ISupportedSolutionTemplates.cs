namespace Wingman.Tool.Generation.Templates
{
    using System.Threading.Tasks;

    public interface ISupportedSolutionTemplates
    {
        Task<bool> IsSupported(string projectType);
    }
}