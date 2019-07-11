namespace Wingman.Tool.Api
{
    using Wingman.Tool.Generation;

    public interface IToolApiClient
    {
        bool IsSupported(string projectType);

        FileTreeTemplate FileTreeTemplateFor(string projectType);

        string RenderFile(string projectType, string projectName, string relativePath);
    }
}