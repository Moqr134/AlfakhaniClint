using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace DemoWAS.Service
{
    public interface ICategoryService
    {
        public Task<HttpResponseMessage> GetCategories();
        public Task<HttpResponseMessage> GetItemByCatigoryId(int id);
        public Task<HttpResponseMessage> GetItems();
    }
    public class CategoryService: ICategoryService
    {
        private readonly HttpClient HttpClient;
        public CategoryService(HttpClient client)
        {
            HttpClient = client;
        }

        public async Task<HttpResponseMessage> GetCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Category/GetAllCategories");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetItemByCatigoryId(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Item/GetItemByCategoryId/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetItems()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Item/GetAllItems");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }
    }
}
