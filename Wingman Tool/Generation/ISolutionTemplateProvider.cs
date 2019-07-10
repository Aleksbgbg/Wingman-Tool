namespace Wingman.Tool.Generation
{
    using Wingman.Tool.Cmd;

    public interface ISolutionTemplateProvider
    {
        FileTreeTemplate TemplateFor(ProjectType solutionType);

        string ContentsFor(ProjectType solutionType, FileTreeEntry file);
    }
}