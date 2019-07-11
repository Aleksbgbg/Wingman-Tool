namespace Wingman.Tool.Api
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Wingman.Tool.Generation;

    public class ToolApiClient : IToolApiClient
    {
        private const string BaseAddress = "http://localhost:53371/wingman-tool/template/";

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
            string isSupported = await _httpClient.GetStringAsync($"is-supported/{projectType}");
            return bool.Parse(isSupported);
        }

        public async Task<FileTreeTemplate> FileTreeTemplateFor(string projectType)
        {
            string templateString = await _httpClient.GetStringAsync($"{projectType}");
            return JsonConvert.DeserializeObject<FileTreeTemplate>(templateString);
        }

        public async Task<string> RenderFile(string projectType, string projectName, string relativePath)
        {
            return await _httpClient.GetStringAsync($"file/{projectType}/{projectName}?{nameof(relativePath)}={relativePath}");
        }
    }
}