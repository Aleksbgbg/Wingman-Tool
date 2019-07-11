namespace Wingman.Tool.Generation.Git
{
    public interface ICommandLineExecutor
    {
        void ExecuteCommandInDirectoryWithArguments(string command, string directory, string arguments);
    }
}