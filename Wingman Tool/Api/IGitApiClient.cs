namespace Wingman.Tool.Api
{
    using System.Threading.Tasks;

    public interface IGitApiClient
    {
        Task<string> GetGitAttributes();

        Task<string> GetGitIgnore();
    }
}