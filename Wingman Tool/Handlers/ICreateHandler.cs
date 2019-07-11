namespace Wingman.Tool.Handlers
{
    using Wingman.Tool.Handlers.Options;

    public interface ICreateHandler
    {
        int HandleAndReturnExitCode(CreateOptions options);
    }
}