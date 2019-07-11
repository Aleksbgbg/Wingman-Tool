namespace Wingman.Tool.Generation
{
    using System.Threading.Tasks;

    public interface IProjectGenerator
    {
        Task GenerateProject(string projectName);

        void InitGit();

        void AddReadme(string projectName, string description);

        void AddRemote(string url);
    }
}