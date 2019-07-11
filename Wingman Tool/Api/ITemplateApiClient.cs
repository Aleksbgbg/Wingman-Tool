namespace Wingman.Tool.Api
{
    using System.Threading.Tasks;

    using Wingman.Tool.Generation;

    public interface ITemplateApiClient
    {
        Task<bool> IsSupported(string projectType);

        Task<FileTreeTemplate> FileTreeTemplateFor(string projectType);

        Task<string> RenderFile(string projectType, string projectName, string relativePath);
    }
}