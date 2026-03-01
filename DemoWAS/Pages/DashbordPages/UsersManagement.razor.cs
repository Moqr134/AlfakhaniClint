using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class UsersManagement
    {
        private List<UserOut> userOuts = new List<UserOut>();
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private string SearchEmail { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var response = await UserService.GetAllUsers();
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                if (content != null)
                {
                    var users = await content.ReadFromJsonAsync<List<UserOut>>();
                    if (users != null)
                    {
                        userOuts = users;
                    }
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "حدث خطا");
            }
        }
        private async Task MakeUserAdmin(int id)
        {
            var response = await UserService.MakeUserAdmin(id);
            if (response.IsSuccessStatusCode)
            {
                await JSRuntime.InvokeVoidAsync("alart", "تم تحويل المستخدم الى أدمن");
                NavigationManager.Refresh();
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "حدث خطا ما");
            }
        }
        private async Task MakeAdminUser(int id)
        {
            var response = await UserService.MakeAdminUser(id);
            if (response.IsSuccessStatusCode)
            {
                await JSRuntime.InvokeVoidAsync("alart", "تم تحويل ألادمن الى مستخدم عادي");
                NavigationManager.Refresh();
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "حدث خطا ما");
            }
        }
        private async Task SearchUserByName()
        {
            if (string.IsNullOrWhiteSpace(SearchEmail))
            {
                await JSRuntime.InvokeVoidAsync("alartError", "الرجاء إدخال الاسم للبحث");
                return;
            }
            var response = await UserService.GetUsersByName(SearchEmail);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                if (content != null)
                {
                    var users = await content.ReadFromJsonAsync<List<UserOut>>();
                    if (users != null && users.Count > 0)
                    {
                        userOuts = users;
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alartError", "لا يوجد مستخدمين بهذا الأسم");
                    }
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "حدث خطا في البحث عن المستخدمين");
            }
        }
    }
}
