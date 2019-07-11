namespace Wingman.Tool.Generation
{
    using System.Threading.Tasks;

    using Wingman.Tool.Api;

    public class SolutionTemplateProvider : ISupportedSolutionTemplates, ISolutionTemplateProvider
    {
        private readonly IToolApiClient _toolApiClient;

        public SolutionTemplateProvider(IToolApiClient toolApiClient)
        {
            _toolApiClient = toolApiClient;
        }

        public Task<bool> IsSupported(string projectType)
        {
            return _toolApiClient.IsSupported(projectType);
        }

        public Task<FileTreeTemplate> TemplateFor(string projectType)
        {
            return _toolApiClient.FileTreeTemplateFor(projectType);
        }

        public async Task<RenderedFileTreeEntry> RenderFileTreeEntry(string projectType, string projectName, FileTreeEntry entry)
        {
            string fileContents = await _toolApiClient.RenderFile(projectType, projectName, entry.RelativePath);

            return new RenderedFileTreeEntry(relativePath: ReplaceProjectNameTokens(entry.RelativePath, projectName),
                                             isDirectory: entry.IsDirectory,
                                             contents: fileContents
            );
        }

        private string ReplaceProjectNameTokens(string relativePath, string projectName)
        {
            return relativePath.Replace("{projectName}", projectName);
        }
    }
}