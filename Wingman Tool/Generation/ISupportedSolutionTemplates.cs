namespace Wingman.Tool.Generation
{
    public interface ISupportedSolutionTemplates
    {
        bool IsSupported(string projectType);
    }
}