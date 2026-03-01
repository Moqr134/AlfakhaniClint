using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using SherdProject.DTO;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace DemoWAS.Service
{
    
    public interface IUserService
    {
        public Task<HttpResponseMessage> Login(UserModel user);
        public Task<HttpResponseMessage> UserOnly();
        public Task<HttpResponseMessage> AdminOnly();
        public Task<HttpResponseMessage> ManegerOnly();
        public Task<HttpResponseMessage> RefreshTokin();
        public Task<HttpResponseMessage> Logout();
        public Task<HttpResponseMessage> Register(UserModel user);
        public Task<HttpResponseMessage> Conform(UserModel user);
        public Task<HttpResponseMessage> GetUserInfo();
        public Task<HttpResponseMessage> GetAllUsers();
        public Task<HttpResponseMessage> MakeUserAdmin(int id);
        public Task<HttpResponseMessage> MakeAdminUser(int id);
        public Task<HttpResponseMessage> GetUsers(string Email);
        public Task<HttpResponseMessage> GetUsersByName(string name);
        public Task<HttpResponseMessage> SaveToken(string Token);
    }
    public class UserService : IUserService
    {
        private readonly HttpClient HttpClient;

        public UserService(HttpClient client)
        {
            HttpClient = client;
        }
        public Task<HttpResponseMessage> UserOnly()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/account/UserOnly");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> Login(UserModel user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/login")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Logout()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/Logout");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> RefreshTokin()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/refresh-token");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Register(UserModel user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/Register")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Conform(UserModel users)
        {
            var json = JsonSerializer.Serialize(users);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/Confirm")
            {
                Content = content
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> GetUserInfo()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/User/GetMyInfo");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
            
        }

        public async Task<HttpResponseMessage> AdminOnly()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/account/AdminOnly");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public Task<HttpResponseMessage> ManegerOnly()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/account/ManagerOnly");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return HttpClient.SendAsync(request);
        }


        public async Task<HttpResponseMessage> GetAllUsers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/User/GetUserList");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await HttpClient.SendAsync(request);
        }

        public Task<HttpResponseMessage> MakeUserAdmin(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/User/MakeUserAdmin/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return HttpClient.SendAsync(request);
        }

        public Task<HttpResponseMessage> MakeAdminUser(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/User/MakeAdminUser/{id}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return HttpClient.SendAsync(request);
        }

        public Task<HttpResponseMessage> GetUsersByName(string Name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/User/GetUserByName/{Name}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return HttpClient.SendAsync(request);
        }

        public Task<HttpResponseMessage> GetUsers(string Email)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/User/GetUserByEmail/{Email}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> SaveToken(string Token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/account/SaveToken/{Token}");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await HttpClient.SendAsync(request);
            return response;
        }
    }
}
