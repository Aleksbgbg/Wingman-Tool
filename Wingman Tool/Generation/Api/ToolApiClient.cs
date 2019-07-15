namespace Wingman.Tool.Generation.Api
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Wingman.Tool.Generation.Templates;

    public class ToolApiClient : ITemplateApiClient, IGitApiClient
    {
#if USE_GLOBAL_SERVER
        private const string BaseAddress = "https://api.iamaleks.dev/wingman-tool/";
#else
        private const string BaseAddress = "http://localhost:53371/wingman-tool/";
#endif

        private readonly HttpClient _httpClient;

        public ToolApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public async Task<bool> IsSupported(string projectType)
        {
            string isSupported = await GetTemplate($"is-supported/{projectType}");
            return bool.Parse(isSupported);
        }

        public async Task<FileTreeTemplate> FileTreeTemplateFor(string projectType)
        {
            string templateString = await GetTemplate(projectType);
            return JsonConvert.DeserializeObject<FileTreeTemplate>(templateString);
        }

        public Task<string> RenderFile(string projectType, string projectName, string relativePath)
        {
            return GetTemplate($"file/{projectType}/{projectName}?{nameof(relativePath)}={relativePath}");
        }

        private Task<string> GetTemplate(string url)
        {
            return GetString($"template/{url}");
        }

        public Task<string> GetGitAttributes()
        {
            return GetGit("attributes");
        }

        public Task<string> GetGitIgnore()
        {
            return GetGit("ignore");
        }

        private Task<string> GetGit(string url)
        {
            return GetString($"git/{url}");
        }

        private Task<string> GetString(string url)
        {
            return _httpClient.GetStringAsync(url);
        }
    }
}