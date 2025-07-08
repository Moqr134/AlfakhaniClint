using DemoWAS.DTO;
using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DemoWAS.Pages.MainPages
{
    public partial class Menu
    {
        private int Counter { get; set; } = 0;
        private List<CategoryDto> Categoris { get; set; } = new List<CategoryDto>();
        private List<ItemDto> Items { get; set; } = new List<ItemDto>();
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;
        [Inject] public CartService CartService { get; set; } = default!;
        [Inject] private IJSRuntime js { get; set; } = default!;
        private List<string> ImigesUrl { get; set; } = new List<string>();
        private void Add(ItemDto item)
        {
            Counter++;
            js.InvokeVoidAsync("ShowCartItem");
            CartService.AddToCart(new CartDto
            {
                Name = item.ItemName,
                Price = item.Price,
            });
        }
        private void cart()
        {
            NavigationManager.NavigateTo("/cart");
        }
        protected override async Task OnInitializedAsync()
        {
        var categories = await CategoryService.GetCategories();
            if (categories.IsSuccessStatusCode && categories.Content != null)
            {
                var categoryList = await categories.Content.ReadFromJsonAsync<List<CategoryDto>>();
                if (categoryList != null)
                {
                    Categoris = categoryList;
                    await GetItems();
                }
            }
        }
        private async Task GetItems()
        {
            var itemsResponse = await CategoryService.GetItems();
            if (itemsResponse.IsSuccessStatusCode && itemsResponse.Content != null)
            {
                var itemList = await itemsResponse.Content.ReadFromJsonAsync<List<ItemDto>>();
                if (itemList != null)
                {
                    Items = itemList;
                }
            }
        }
    }
}
