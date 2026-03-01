using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.MainPages
{
    public partial class Menu
    {
        private List<CategoryDto> categoryDtos { get; set; } = new List<CategoryDto>();
        private List<ItemDto> Items { get; set; } = new List<ItemDto>();
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;
        [Inject] private IItemService ItemService { get; set; } = default!;
        [Inject] public CartService CartService { get; set; } = default!;
        [Inject] private IJSRuntime js { get; set; } = default!;
        private DotNetObjectReference<Menu>? dotNetRef;
        private SizeDto? Size { get; set; } = default!;
        private int SelectedSizeId { get; set; } = 0;
        private int SelectedCategoryId { get; set; } = 0;
        private bool IsLoading { get; set; } = true;
        private bool LodingItems { get; set; } = true;
        private bool IsDone { get; set; } = true;
        private double WightQuantity { get; set; } = 0;
        private PageDto Page { get; set; } = new PageDto
        {
            pageIndex = 1,
            pageSize = 6
        };
        public async ValueTask DisposeAsync()
        {
            await js.InvokeVoidAsync("removeScrollListener");
            dotNetRef?.Dispose();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                dotNetRef = DotNetObjectReference.Create(this);
                await js.InvokeVoidAsync("registerScrollHandler", dotNetRef, nameof(OnPageChanged));
            }
        }
        [JSInvokable]
        public async Task OnPageChanged()
        {
            if (IsDone) return;
            Page.pageIndex++;
            await GetItems(SelectedCategoryId);
            StateHasChanged();
        }
        private void Add(ItemDto item)
        {

            var hour = DateTime.UtcNow.AddHours(3).Hour;

            if (hour < 9 || hour >= 24)
            {
                js.InvokeVoidAsync("alartError",
                    "عذرا المحل مغلق حاليا. يمكنكم الطلب بعد الساعة 9 صباحا وقبل الساعة 12 مسائاً");
                return;
            }
            if (SelectedSizeId<0)
            {
                js.InvokeVoidAsync("alartError", "الرجاء اختيار الحجم");
                return;
            }
            if (item.Type == "weight")
            {
                if (WightQuantity <= 0)
                {
                    js.InvokeVoidAsync("alartError", "الرجاء ادخال الوزن");
                    return;
                }
            }
            if (item.Sizes.Count == 0)
            {
                CartDto cartDto = new CartDto
                {
                    Id = item.Id,
                    Name = item.ItemName,
                    Price = item.Price,
                    Type = item.Type,
                };
                if (item.Type == "weight")
                {
                    cartDto.Quantity = WightQuantity;
                    WightQuantity = 0;
                }
                CartService.AddToCart(cartDto);
            }
            else
            {
                Size = item.Sizes.FirstOrDefault(s => s.Id == SelectedSizeId);
                if (Size == null)
                {
                    js.InvokeVoidAsync("alartError", "الحجم غير موجود");
                    return;
                }
                CartService.AddToCart(new CartDto
                {
                    Id = item.Id,
                    Name = item.ItemName+" "+Size.Name,
                    Price = Size.Price,
                    Type = item.Type,
                });
            }
        }
        private void cart()
        {
            NavigationManager.NavigateTo("/cart");
        }
        protected override async Task OnInitializedAsync()
        {
            var response = await CategoryService.GetCategories();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
                if (categories != null)
                {
                    categoryDtos = categories;
                }
            }
            else
            {
                IsDone=false;
                IsLoading=false;
                return;
            }
            IsLoading = false;
            SelectedCategoryId = categoryDtos[0].Id;
            await GetItems(SelectedCategoryId);
            IsDone = false;
        }
        private async Task OnCategoryChange(int id)
        {
            SelectedSizeId = 0;
            Size = null;
            Page.pageIndex = 1;
            Page.pageSize = 6;
            IsDone = false;
            SelectedCategoryId = id;
            LodingItems = true;
            Items.Clear();
            await GetItems(SelectedCategoryId);
        }
        private async Task GetItems(int id)
        {
            LodingItems = true;
            IsDone = true;
            var NewItems = await ItemService.GetAllItemsByCategoryId(id,Page);
            if(Page.pageIndex>1 && Items.Count > 0 && NewItems.Count > 0 && NewItems[0].Id == Items[0].Id)
            {
                IsDone = true;
                LodingItems = false;
                return;
            }
            if (NewItems.Count != 0)
            {
                Items.AddRange(NewItems);
                IsDone = false;
            }
            else IsDone = true;
            if(NewItems.Count > Page.pageSize)
            {
                IsDone = true;
            }
            if (Items == null || Items.Count == 0)
            {
                await js.InvokeVoidAsync("alartError", "لا توجد عناصر في هذا القسم");
                LodingItems = false;
            }
            else
            {
                LodingItems = false;
            }
        }
        private void NavigateToProduct(int id)
        {
            NavigationManager.NavigateTo($"/product/{id}");
        }
    }
}
