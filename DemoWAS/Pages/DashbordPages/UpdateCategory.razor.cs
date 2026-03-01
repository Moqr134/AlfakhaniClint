using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class UpdateCategory
    {
        [Parameter]
        public int id { get; set; }
        [Inject] private ICategoryService CategoryService { get; set; }= default!;
        [Inject] public NavigationManager NavigationManager { get; set; }= default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private CategoryDto category = new();
        protected override async Task OnInitializedAsync()
        {
            var requst = await CategoryService.GetCategory(id);
            if (requst.IsSuccessStatusCode && requst.Content != null)
            {
                var categories = await requst.Content.ReadFromJsonAsync<CategoryDto>();
                if (categories != null)
                {
                    category = categories;
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alartError", "Failed to parse category.");
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "Failed to load category.");
            }
        }
        private async Task UpdateCategoryAsync()
        {
            if (category != null && !string.IsNullOrWhiteSpace(category.CategoryName))
            {
                var response = await CategoryService.UpdateCategory(category);
                string massege = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeVoidAsync("alart", massege);
                    NavigationManager.NavigateTo("/categorymanagement");
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alartError", massege);
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "يرجى ادخال البيانات بصورة صحيحة");
            }
        }
    }
}
