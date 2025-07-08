using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DemoWAS.Layout
{
    public partial class MainLayout
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var response = await UserService.UserOnly();
            if (!response.IsSuccessStatusCode)
            {
                var res = await UserService.RefreshTokin();
                if (!res.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeVoidAsync("alartError", "ليست لديك الصلاحية للدخول الى هنا يرجى تسجيل الدخول");
                    NavigationManager.NavigateTo("/login");
                } 
            }
        }
    }
}
