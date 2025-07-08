
using DemoWAS.DTO;
using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DemoWAS.Layout
{
    public partial class NavMenu
    {
        [Inject]
        private IUserService UserService { get; set; } = default!;
        [Inject]
        private NavigationManager navigationManager { get; set; }= default!;
        [Inject]
        private IJSRuntime jS { get; set; }
        private UserOut userOut = new();
        protected override async Task OnInitializedAsync()
        {
            var response = await UserService.GetUserInfo();
            if (response.IsSuccessStatusCode)
            {
                var userOutTask = response.Content.ReadFromJsonAsync<UserOut>();
                if (userOutTask != null)
                {
                    var result = await userOutTask;
                    if (result != null)
                    {
                        userOut = result;
                    }
                }
            }
        }
        private async Task Logout()
        {
            var response = await UserService.Logout();
            if (response.IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/login");
            }
            else
            {
                await jS.InvokeVoidAsync("alart", "حدث خطا اثناء تسجيل الخروج");
            }
        }
    }
}
