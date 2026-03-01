using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
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
            if (string.IsNullOrEmpty(user.Username))
            {
                await js.InvokeVoidAsync("alartError", "يرجى إدخال اسم المستخدم.");
                return;
            }
            else if (string.IsNullOrEmpty(user.Password))
            {
                await js.InvokeVoidAsync("alartError", "يرجى إدخال كلمة المرور.");
                return;
            }
            if (user.Password != ConformPassword)
            {
                await js.InvokeVoidAsync("alart", "تأكيد كلمة المرور لا يطابق كلمة المرور");
                return;
            }
            var response = await userService.Register(user);
            string massege = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await js.InvokeVoidAsync("alart", "تم تأكيد الحساب بنجاح يرجى تسجيل الدخول");
                nav.NavigateTo("/login");
            }
            else
            {
                await js.InvokeVoidAsync("alart", massege.ToString());
            }
        }
    }
}
