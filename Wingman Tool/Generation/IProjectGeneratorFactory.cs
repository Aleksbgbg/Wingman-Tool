namespace Wingman.Tool.Generation
{
    using Wingman.Tool.Cmd;

    public interface IProjectGeneratorFactory
    {
        IProjectGenerator CreateGeneratorFor(ProjectType projectType);
    }
}