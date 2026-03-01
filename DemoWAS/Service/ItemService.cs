using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using SherdProject.DTO;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DemoWAS.Service
{
    public interface IItemService
    {
        public Task<HttpResponseMessage> AddItem(ItemDto item,IBrowserFile file);
        public Task<HttpResponseMessage> GetAllItemsAsync(PageDto pageDto);
        public Task<List<ItemDto>> GetAllItemsByCategoryId(int categoryId,PageDto dto);
        public Task<HttpResponseMessage> GetItem(int id);
        public Task<HttpResponseMessage> DeleteItem(int id);
        public Task<HttpResponseMessage> UpdateItem(ItemDto item,IBrowserFile file);
        public Task<HttpResponseMessage> SerchItem(string itemName);
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

        public async Task<HttpResponseMessage> AddItem(ItemDto item, IBrowserFile file)
        {
            using var Filecontent = new MultipartFormDataContent();
            Filecontent.Add(
                new StreamContent(file.OpenReadStream(5 * 1024 * 1024)),
                "formFile",
                file.Name);
            Filecontent.Add(new StringContent(item.ItemName ?? ""), "ItemName");
            Filecontent.Add(new StringContent(item.Description ?? ""), "Description");
            Filecontent.Add(new StringContent(item.CategoryId.ToString()), "CategoryId");
            Filecontent.Add(new StringContent(item.Type ?? ""), "Type");
            Filecontent.Add(new StringContent(item.Price.ToString()), "Price");
            Filecontent.Add(new StringContent(item.Shwoing.ToString()), "Shwoing");
            Filecontent.Add(new StringContent(item.ItemImage ?? ""), "ItemImage");
            Filecontent.Add(new StringContent(item.ImageContentType ?? ""), "ImageContentType");
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Item/AddNewItem")
            {
                Content = Filecontent,
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

        public async Task<HttpResponseMessage> GetAllItemsAsync(PageDto pageDto)
        {
            var json = JsonSerializer.Serialize(pageDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Item/GetAllItems")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<List<ItemDto>> GetAllItemsByCategoryId(int categoryId,PageDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Item/GetItemByCategoryId/{categoryId}")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                if (response.Content == null)
                {
                    return new List<ItemDto>();
                }
                var items = await response.Content.ReadFromJsonAsync<List<ItemDto>>();
                return items ?? new List<ItemDto>();
            }
            else
            {
                return new List<ItemDto>();
            }
        }

        public async Task<HttpResponseMessage> GetItem(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Item/GetItem/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await _httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> SerchItem(string itemName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Item/GetItemByName/{itemName}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateItem(ItemDto item,IBrowserFile? file)
        {
            using var Filecontent = new MultipartFormDataContent();
            if (file != null)
            {
                Filecontent.Add(
                    new StreamContent(file.OpenReadStream(5 * 1024 * 1024)),
                    "file",
                    file.Name);
            }
            Filecontent.Add(new StringContent(item.Id.ToString()), "Id");
            Filecontent.Add(new StringContent(item.ItemName ?? ""), "ItemName");
            Filecontent.Add(new StringContent(item.Description ?? ""), "Description");
            Filecontent.Add(new StringContent(item.CategoryId.ToString()), "CategoryId");
            Filecontent.Add(new StringContent(item.Type ?? ""), "Type");
            Filecontent.Add(new StringContent(item.Price.ToString()), "Price");
            Filecontent.Add(new StringContent(item.Shwoing.ToString()), "Shwoing");
            Filecontent.Add(new StringContent(item.ItemImage ?? ""), "ItemImage");
            Filecontent.Add(new StringContent(item.ImageContentType ?? ""), "ImageContentType");
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Item/UpdateItem")
            {
                Content = Filecontent,
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            return response;
        }
    }
}
