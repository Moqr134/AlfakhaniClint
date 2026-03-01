using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DemoWAS.Layout
{
    public partial class MainLayout
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private HubConnection? _hub;
        private readonly List<string> _messages = new();
        //protected override async Task OnInitializedAsync()
        //{
        //    var response = await UserService.UserOnly();
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var res = await UserService.RefreshTokin();
        //        if (!res.IsSuccessStatusCode)
        //        {
        //            await JSRuntime.InvokeVoidAsync("alartError", "ليست لديك الصلاحيات المناسبة");
        //            NavigationManager.NavigateTo("/login");
        //        }
        //    }
        //    _hub = new HubConnectionBuilder()
        //     .WithUrl("https://alfakhaniapi.premiumasp.net/notificationhub")
        //    //.WithUrl("https://localhost:7292/notificationhub")
        //    .Build();

        //    _hub.On<string>("ReceiveNotification", async message =>
        //    {
        //        _messages.Add(message);
        //        await JSRuntime.InvokeVoidAsync("alartNotification", message);
        //    });

        //    await _hub.StartAsync();
        //}
        //public async ValueTask DisposeAsync()
        //{
        //    if (_hub is not null)
        //    {
        //        await _hub.DisposeAsync();
        //    }
        //}
    }
}
