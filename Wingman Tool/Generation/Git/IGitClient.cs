namespace Wingman.Tool.Generation.Git
{
    public interface IGitClient
    {
        void Init();

        void AddAll();

        void Commit(string commitMessage);

        void AddRemote(string url);

        void Push();
    }
}