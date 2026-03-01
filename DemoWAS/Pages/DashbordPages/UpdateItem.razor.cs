using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class UpdateItem
    {
        [Parameter] public int id { get; set; }
        [Inject] private IItemService ItemService { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;
        private ItemDto item { get; set; } = new ItemDto();
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        private List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        private IBrowserFile File { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
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
            var itemRequest = await ItemService.GetItem(id);
            if (itemRequest.IsSuccessStatusCode && itemRequest.Content != null)
            {
                var Item = await itemRequest.Content.ReadFromJsonAsync<ItemDto>();
                if (Item != null)
                {
                    item = Item;
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alartError", "Failed to parse Item.");
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "Failed to load Item.");
            }
        }
        private void OnSwitchCange()
        {
            if (item.Shwoing)
                item.Shwoing = false;
            else item.Shwoing = true;
        }
        private async Task ImageSelected(InputFileChangeEventArgs e)
        {
            if (e.File != null && e.File.Size > 0)
            {
                item.ItemImage = e.File.Name;
                item.ImageContentType = e.File.ContentType;
                File = e.File;
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "قم بأختيار صورة");
            }
            
        }
        public async Task UpdateItemInfo(ItemDto item)
        {
            if (item.CategoryId == 0) item.CategoryId = Categories[0].Id;
            if (item != null && !string.IsNullOrWhiteSpace(item.ItemName) && item.Price > 0 && !string.IsNullOrEmpty(item.Description))
            {
                var response = await ItemService.UpdateItem(item,File);
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
    }
}
