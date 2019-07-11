namespace Wingman.Tool.Generation.Api
{
    using System.Threading.Tasks;

    using Wingman.Tool.Generation.Templates;

    public interface ITemplateApiClient
    {
        Task<bool> IsSupported(string projectType);

        Task<FileTreeTemplate> FileTreeTemplateFor(string projectType);

        Task<string> RenderFile(string projectType, string projectName, string relativePath);
    }
}