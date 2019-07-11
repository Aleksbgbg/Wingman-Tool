namespace Wingman.Tool.Generation
{
    using Wingman.Tool.Api;

    public class SolutionTemplateProvider : ISupportedSolutionTemplates, ISolutionTemplateProvider
    {
        private readonly IToolApiClient _toolApiClient;

        public SolutionTemplateProvider(IToolApiClient toolApiClient)
        {
            _toolApiClient = toolApiClient;
        }

        public bool IsSupported(string projectType)
        {
            return _toolApiClient.IsSupported(projectType);
        }

        public FileTreeTemplate TemplateFor(string projectType)
        {
            return _toolApiClient.FileTreeTemplateFor(projectType);
        }

        public RenderedFileTreeEntry RenderFileTreeEntry(string projectType, string projectName, FileTreeEntry entry)
        {
            string fileContents = _toolApiClient.RenderFile(projectType, projectName, entry.RelativePath);

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