using DemoWAS.DTO;
using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class AddNewItem
    {
        [Inject] private IItemService ItemService { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;
        private ItemDto item { get; set; } = new ItemDto();
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        private List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private async Task AddItem()
        {
            if (item.CategoryId == 0) item.CategoryId = Categories[0].Id;
            if (item != null && !string.IsNullOrWhiteSpace(item.ItemName) && item.Price > 0 && !string.IsNullOrEmpty(item.Description))
            {
                var response = await ItemService.AddItem(item);
                string massege = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeVoidAsync("alart", massege);
                    NavigationManager.NavigateTo("/menumanagement");
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
        protected override async Task OnInitializedAsync()
        {
            var requst = await CategoryService.GetCategories();
            if (requst.IsSuccessStatusCode && requst.Content != null)
            {
                var categories = await requst.Content.ReadFromJsonAsync<List<CategoryDto>>();
                if (categories != null)
                {
                    Categories = categories;
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alartError", "Failed to parse categories.");
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "Failed to load categories.");
            }
        }
        private async Task ImageSelected(InputFileChangeEventArgs e)
        {
            var fileformat = "image/png";
            var file = await e.File.RequestImageFileAsync(fileformat,300,300);
            var filebytes = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(filebytes);
            var imageBase64 = $"data:{fileformat};base64,{Convert.ToBase64String(filebytes)}";

            if (file != null && file.Size > 0)
            {
                item.ItemImage = imageBase64;
                item.ImageContentType = file.ContentType;
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "قم بأختيار صورة");
            }
        }
    }
}
