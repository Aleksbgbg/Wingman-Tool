namespace Wingman.Tool.Api
{
    using System.Threading.Tasks;

    public interface IGitApiClient
    {
        Task<string> GitAttributes();

        Task<string> GitIgnore();
    }
}