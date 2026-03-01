using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class AddCategory
    {
        [Inject] private ICategoryService CategoryService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        private CategoryDto Category { get; set; } = new CategoryDto();
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private async Task AddCategoryAsync()
        {
            if (!string.IsNullOrWhiteSpace(Category.CategoryName))
            {
                var response = await CategoryService.AddCategory(Category);
                string message = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeVoidAsync("alart", message);
                    NavigationManager.NavigateTo("/categorymanagement");
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alartError", message);
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "يرجى ادخال اسم الصنف");
            }
        }
    }
}
