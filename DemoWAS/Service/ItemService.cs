using DemoWAS.DTO;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Text;
using System.Text.Json;

namespace DemoWAS.Service
{
    public interface IItemService
    {
        public Task<HttpResponseMessage> AddItem(ItemDto item);
        public Task<HttpResponseMessage> GetAllItemsAsync();
        public Task<HttpResponseMessage> GetItem(int id);
        public Task<HttpResponseMessage> DeleteItem(int id);
        public Task<HttpResponseMessage> UpdateItem(ItemDto item);
    }
    public class ItemService : IItemService
    {
        private readonly HttpClient _httpClient;
        public ItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> AddItem(ItemDto item)
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Item/AddNewItem")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteItem(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Item/DeleteItem/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> GetAllItemsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Item/GetAllItems");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await _httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetItem(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Item/GetItem/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await _httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> UpdateItem(ItemDto item)
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Item/UpdateItem")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response;
        }
    }
}
