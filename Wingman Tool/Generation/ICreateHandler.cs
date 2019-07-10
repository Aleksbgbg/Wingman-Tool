namespace Wingman.Tool.Generation
{
    using Wingman.Tool.Cmd;

    public interface ICreateHandler
    {
        int HandleAndReturnExitCode(CreateOptions options);
    }
}