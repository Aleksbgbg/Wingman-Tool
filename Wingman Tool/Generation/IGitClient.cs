namespace Wingman.Tool.Generation
{
    public interface IGitClient
    {
        void Init();

        void AddRemote(string url);
    }
}