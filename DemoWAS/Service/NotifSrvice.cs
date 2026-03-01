using Microsoft.AspNetCore.Components.WebAssembly.Http;
using SherdProject.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DemoWAS.Service
{
    public interface INotifSrvice
    {
        public Task<List<NotifecationDto>?> GetAll();
        public Task<bool> DeleteNotif(int id);
    }
    public class NotifSrvice : INotifSrvice
    {
        private readonly HttpClient _httpClient;
        public NotifSrvice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteNotif(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Notif/DeleteNotif/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<NotifecationDto>?> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Notif/GetNotif");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var responce = await _httpClient.SendAsync(request);
            if (responce.IsSuccessStatusCode)
            {
                var data = await responce.Content.ReadFromJsonAsync<List<NotifecationDto>>();
                if (data != null && data.Count > 0)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
