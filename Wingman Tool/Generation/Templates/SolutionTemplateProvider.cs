namespace Wingman.Tool.Generation.Templates
{
    using System.Threading.Tasks;

    using Wingman.Tool.Generation.Api;

    public class SolutionTemplateProvider : ISupportedSolutionTemplates, ISolutionTemplateProvider
    {
        private readonly ITemplateApiClient _templateApiClient;

        public SolutionTemplateProvider(ITemplateApiClient templateApiClient)
        {
            _templateApiClient = templateApiClient;
        }

        public Task<bool> IsSupported(string projectType)
        {
            return _templateApiClient.IsSupported(projectType);
        }

        public Task<FileTreeTemplate> TemplateFor(string projectType)
        {
            return _templateApiClient.FileTreeTemplateFor(projectType);
        }

        public async Task<RenderedFileTreeEntry> RenderFileTreeEntry(string projectType, string projectName, FileTreeEntry entry)
        {
            string fileContents = null;

            if (!entry.IsDirectory)
            {
                fileContents = await _templateApiClient.RenderFile(projectType, projectName, entry.RelativePath);
            }

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