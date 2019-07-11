namespace Wingman.Tool.Generation
{
    public interface ISolutionTemplateProvider
    {
        FileTreeTemplate TemplateFor(string projectType);

        RenderedFileTreeEntry RenderFileTreeEntry(string projectType, string projectName, FileTreeEntry entry);
    }
}