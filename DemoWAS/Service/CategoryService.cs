using Microsoft.AspNetCore.Components.WebAssembly.Http;
using SherdProject.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DemoWAS.Service
{
    public interface ICategoryService
    {
        public Task<HttpResponseMessage> GetCategories(PageDto pageDto);
        public Task<HttpResponseMessage> GetCategory(int id);
        public Task<HttpResponseMessage> GetItemByCatigoryId(int id);
        public Task<HttpResponseMessage> GetItems(PageDto pageDto);
        public Task<HttpResponseMessage> AddCategory(CategoryDto category);
        public Task<HttpResponseMessage> UpdateCategory(CategoryDto category);
        public Task<HttpResponseMessage> GetCategories();
    }
    public class CategoryService: ICategoryService
    {
        private readonly HttpClient HttpClient;
        public CategoryService(HttpClient client)
        {
            HttpClient = client;
        }

        public async Task<HttpResponseMessage> AddCategory(CategoryDto category)
        {
            var json = JsonSerializer.Serialize(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Category/AddCategory")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> GetCategories(PageDto pageDto)
        {
            var json = JsonSerializer.Serialize(pageDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Category/GetAllCategories")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> GetCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Category/GetCategories");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetCategory(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Category/GetCategory/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetItemByCatigoryId(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Item/GetItemByCategoryId/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetItems(PageDto pageDto)
        {
            var json = JsonSerializer.Serialize(pageDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Item/GetAllItems")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateCategory(CategoryDto category)
        {
            var json = JsonSerializer.Serialize(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Category/UpdateCategory")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }
    }
}
