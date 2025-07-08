using DemoWAS.DTO;
using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class MenuMangmant
    {
        [Inject] private IItemService itemService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        private List<ItemDto> items { get; set; } = new List<ItemDto>();
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            var response = await itemService.GetAllItemsAsync();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var content = await response.Content.ReadFromJsonAsync<List<ItemDto>>();
                if (content != null)
                {
                    items = content;
                }
            }
        }
        private async Task DeleteItem(int id)
        {
            var response = await itemService.DeleteItem(id);
            if (response.IsSuccessStatusCode)
            {
                items.RemoveAll(item => item.Id == id);
                await JSRuntime.InvokeVoidAsync("alart", "تم حذف العنصر");
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "حدث خطأ في عمليه حذف العنصر");
            }
        }
        private void AddItem()
        {
            NavigationManager.NavigateTo("/addnewitem");
        }
        private void UpdateItem(int id)
        {
            NavigationManager.NavigateTo($"/updateitem/{id}");
        }
    }
}
