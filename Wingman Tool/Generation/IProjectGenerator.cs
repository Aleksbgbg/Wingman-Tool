namespace Wingman.Tool.Generation
{
    public interface IProjectGenerator
    {
        void GenerateProject(string projectName);

        void InitGit();

        void AddReadme(string description);

        void AddRemote(string remote);
    }
}