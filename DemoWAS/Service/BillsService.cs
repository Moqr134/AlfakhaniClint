using Microsoft.AspNetCore.Components.WebAssembly.Http;
using SherdProject.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DemoWAS.Service
{
    public interface IBillsService
    {
        public Task<HttpResponseMessage> AddBills(BillsDto billsDto);
        public Task<HttpResponseMessage> GetBills();
        public Task<HttpResponseMessage> GetBillById(int id);
        public Task<HttpResponseMessage> GetAllMyBills();
        public Task<HttpResponseMessage> GetManegarBills(PageDto pageDto);
        public Task<HttpResponseMessage> ChangeStatus(ChangeStutesDto changeStutesDto);
        public Task<HttpResponseMessage> CancleOrder(BillsOut billsOut);
    }
    public class BillsService : IBillsService
    {
        private readonly HttpClient HttpClient;
        public BillsService(HttpClient client)
        {
            HttpClient = client;
        }

        public async Task<HttpResponseMessage> AddBills(BillsDto billsDto)
        {
            var json = JsonSerializer.Serialize(billsDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Bills/AddBills")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public Task<HttpResponseMessage> CancleOrder(BillsOut billsOut)
        {
            var json = JsonSerializer.Serialize(billsOut);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Bills/CancleOrder")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> ChangeStatus(ChangeStutesDto changeStutesDto)
        {
            var json = JsonSerializer.Serialize(changeStutesDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Bills/ChangeStutes")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> GetAllMyBills()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Bills/GetMyBills");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetBillById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Bills/GetBillsById{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetBills()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Bills/GetBills");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetManegarBills(PageDto pageDto)
        {
            var json = JsonSerializer.Serialize(pageDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Bills/GetManagerBills")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }
    }
}
