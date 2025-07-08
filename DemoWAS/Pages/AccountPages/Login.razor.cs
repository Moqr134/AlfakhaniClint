using DemoWAS.DTO;
using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
    protected async override void OnInitialized()
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
            var response = await userService.Login(user);

            if (response.IsSuccessStatusCode)
            {
                await js.InvokeVoidAsync("alart", "تم تسجيل الدخول بنجاح!");
                nav.NavigateTo("/");
            }
            else
            {
                await js.InvokeVoidAsync("alart", "فشل تسجيل الدخول. يرجى التحقق من اسم المستخدم وكلمة المرور.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"خطأ: {ex.Message}");
        }
    }
}
