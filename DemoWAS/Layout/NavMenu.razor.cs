using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Layout
{
    public partial class NavMenu
    {
        [Inject]
        private IUserService UserService { get; set; } = default!;
        [Inject]
        private NavigationManager navigationManager { get; set; } = default!;
        [Inject]
        private IJSRuntime jS { get; set; } = default!;
        [Inject] private UserInfoService UserInfoService { get; set; } = default!;
        private UserOut? userOut = new UserOut();
        private HubConnection? _hub;
        private readonly List<string> _messages = new();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (UserInfoService.UserOut.Id != 0)
                {
                    _hub = new HubConnectionBuilder()
                    //.WithUrl("https://api.alfakhani.com/notificationhub")
                    .WithUrl("https://alfakhaniapi.premiumasp.net/notificationhub")
                    //.WithUrl("https://localhost:7292/notificationhub")
                    .Build();

                    _hub.On<string>("ReceiveNotification", async message =>
                    {
                        _messages.Add(message);
                        await jS.InvokeVoidAsync("alartNotification", message);
                    });

                    await _hub.StartAsync();
                }
                else
                {
                    var response = await UserService.UserOnly();
                    if (response.IsSuccessStatusCode)
                    {
                        navigationManager.NavigateTo("/");
                    }
                }
            }
        }
        private async Task Logout()
        {
            var response = await UserService.Logout();
            if (response.IsSuccessStatusCode)
            {
                userOut = null;
                UserInfoService.UserOut = new UserOut();
                StateHasChanged();
                navigationManager.NavigateTo("/login");
            }
            else
            {
                await jS.InvokeVoidAsync("alart", "حدث خطا اثناء تسجيل الخروج");
            }
        }
    }
}
