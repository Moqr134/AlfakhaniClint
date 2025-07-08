using DemoWAS.Service;
using Microsoft.AspNetCore.Components;

namespace DemoWAS.Pages.MainPages
{
    public partial class StartPage 
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            var response = await UserService.UserOnly();
            if (response.IsSuccessStatusCode)
                NavigationManager.NavigateTo("/home");
            else
            {
                var res = await UserService.RefreshTokin();
                if (res.IsSuccessStatusCode)
                    NavigationManager.NavigateTo("/home");
                else
                    NavigationManager.NavigateTo("/login");
            }
        }
    }
}
