using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
namespace DemoWAS.Pages.AccountPages;

public partial class Login
{
    [Inject]
    private IUserService userService { get; set; }= default!;
    [Inject]
    private NavigationManager nav { get; set; }= default!;
    [Inject]
    private IJSRuntime js { get; set; } = default!;

    private UserModel user = new();
    protected override async void OnInitialized()
    {
        var response = await userService.UserOnly();
        if (response.IsSuccessStatusCode)
            nav.NavigateTo("/");
        else
        {
            var res = await userService.RefreshTokin();
            if (res.IsSuccessStatusCode)
                nav.NavigateTo("/");
        }
    }
    private async Task HandelLogin()
    {
        try
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                await js.InvokeVoidAsync("alartError", "يرجى إدخال اسم المستخدم.");
                return;
            }else if (string.IsNullOrEmpty(user.Password))
            {
                await js.InvokeVoidAsync("alartError", "يرجى إدخال كلمة المرور.");
                return;
            }
            var response = await userService.Login(user);
            string message = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await js.InvokeVoidAsync("alart", "تم تسجيل الدخول بنجاح!");
                nav.NavigateTo("/");
            }
            else
            {
                await js.InvokeVoidAsync("alart", message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"خطأ: {ex.Message}");
        }
    }
}
