namespace Wingman.Tool.Generation
{
    public interface ICommandLineExecutor
    {
        void ExecuteCommandInDirectoryWithArguments(string command, string directory, string arguments);
    }
}