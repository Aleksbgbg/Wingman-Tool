namespace Wingman.Tool.Generation
{
    public interface ISolutionTemplateProvider
    {
        FileTreeTemplate TemplateFor(string projectType, string projectName);

        string ContentsFor(string projectType, FileTreeEntry file);
    }
}