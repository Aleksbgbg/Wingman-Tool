namespace Wingman.Tool.Generation
{
    using System.Threading.Tasks;

    public interface IProjectGenerator
    {
        Task GenerateProject(string projectName);

        void InitGit();

        Task AddGitMetadata();

        void AddReadme(string projectName, string description);

        void Commit(string commitMessage);

        void AddRemote(string url);

        void Push();
    }
}