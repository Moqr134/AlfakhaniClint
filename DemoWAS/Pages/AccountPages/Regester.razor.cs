using DemoWAS.DTO;
using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace DemoWAS.Pages.AccountPages
{
    public partial class Regester
    {
        [Inject]
        private IUserService userService { get; set; } = default!;
        [Inject]
        private NavigationManager nav { get; set; } = default!;
        [Inject]
        private IJSRuntime js { get; set; } = default!;
        private readonly UserModel user = new();
        private string? ConformPassword = null;
        private async Task HandelRegestar()
        {
            if (user.Password != ConformPassword)
            {
                await js.InvokeVoidAsync("alart", "Passwords do not match.");
                return;
            }
            var response = await userService.Register(user);
            if (response.IsSuccessStatusCode)
            {
                await js.InvokeVoidAsync("ShowConfirmation");
            }
            else
            {
                await js.InvokeVoidAsync("alart", "Registration failed. Please try again.");
            }
        }
        private async Task HandelConform()
        {
            if(user.code is null)
            {
                await js.InvokeVoidAsync("alart", "Please enter a valid code.");
                return;
            }
            var response = await userService.Conform(user);
            if (response.IsSuccessStatusCode)
            {
                await js.InvokeVoidAsync("alart", "تم تأكيد الحساب بنجاح يرجى تسجيل الدخول");
                nav.NavigateTo("/login");
            }
        }
    }
}
