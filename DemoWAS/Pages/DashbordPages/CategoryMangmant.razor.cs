using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class CategoryMangmant
    {
        private List<CategoryDto> categories = new List<CategoryDto>();
        [Inject] private ICategoryService CategoryService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            var response = await CategoryService.GetCategories();
            if (response.IsSuccessStatusCode)
            {
                var categoryList = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
                if (categoryList != null)
                {
                    categories = categoryList;
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alartError", "لا توجد اصناف");
                }
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                await JSRuntime.InvokeVoidAsync("alartError", errorMessage);
            }
        }
        private void AddCategory()
        {
            NavigationManager.NavigateTo("/AddCategory");
        }
        private void UpdateCategory(int id)
        {
            NavigationManager.NavigateTo($"/update-category/{id}");
        }
    }
}
