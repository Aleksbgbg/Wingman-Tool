namespace Wingman.Tool.Handlers
{
    using Wingman.Tool.Cmd;

    public interface ICreateHandler
    {
        int HandleAndReturnExitCode(CreateOptions options);
    }
}