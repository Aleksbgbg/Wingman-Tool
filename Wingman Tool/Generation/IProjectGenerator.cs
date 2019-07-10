namespace Wingman.Tool.Generation
{
    public interface IProjectGenerator
    {
        void GenerateProject(string name);

        void InitGit();

        void AddReadme(string description);

        void AddRemote(string remote);
    }
}