using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class Dashboard
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private bool IsManager { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            var response = await UserService.AdminOnly();
            if (!response.IsSuccessStatusCode)
            {
                await JSRuntime.InvokeVoidAsync("alartError", "ليست لديك الصلاحية للدخول الى هنا");
                NavigationManager.NavigateTo("/home");
            }
            else
            {
                var newResponse = await UserService.ManegerOnly();
                IsManager = newResponse.IsSuccessStatusCode;
            }
        }
        private  void NavToMenuMangment()
        {
            NavigationManager.NavigateTo("/menumanagement");
        }
    }
}
