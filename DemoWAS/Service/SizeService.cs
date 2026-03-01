using Microsoft.AspNetCore.Components.WebAssembly.Http;
using SherdProject.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DemoWAS.Service
{
    public interface ISizeService
    {
        public Task<HttpResponseMessage> AddSize(SizeDto sizeDto);
        public Task<HttpResponseMessage> DeleteSize(SizeDto sizeDto);
    }
    public class SizeService : ISizeService
    {
        private readonly HttpClient _httpClient;
        public SizeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> AddSize(SizeDto sizeDto)
        {
            var json = JsonSerializer.Serialize(sizeDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Sizes/AddNewSize")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteSize(SizeDto sizeDto)
        {
            var json = JsonSerializer.Serialize(sizeDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/Sizes/DeleteSize")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response;
        }
    }
}
