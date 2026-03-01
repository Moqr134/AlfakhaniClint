using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.MainPages
{
    public partial class Product
    {
        [Parameter]
        public int Id { get; set; }
        private ItemDto Item { get; set; } = new ItemDto();
        [Inject]
        private IItemService ItemService { get; set; } = default!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        private IJSRuntime js { get; set; } = default!;
        private double price;
        protected async override Task OnInitializedAsync()
        {
            var request = await ItemService.GetItem(Id);
            if (request.IsSuccessStatusCode)
            {
                var item = await request.Content.ReadFromJsonAsync<ItemDto>();
                if (item != null)
                {
                    Item = item;
                    price = Item.Price;
                }
                else
                {
                    await js.InvokeVoidAsync("alartError", "Item not found");
                    NavigationManager.NavigateTo("/menu");
                }
            }
            else
            {
                await js.InvokeVoidAsync("alartError", request.Content.ReadAsStringAsync().ToString());
                NavigationManager.NavigateTo("/menu");
            }
        }

    }
}
