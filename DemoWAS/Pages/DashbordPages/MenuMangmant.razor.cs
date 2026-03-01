using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class MenuMangmant
    {
        [Inject] private IItemService itemService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        private List<ItemDto> items { get; set; } = new List<ItemDto>();
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private string Search = string.Empty;
        private DotNetObjectReference<MenuMangmant>? dotNetRef;
        private PageDto pageDto = new PageDto
        {
            pageIndex = 1,
            pageSize = 5
        };
        private bool IsDone { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            await GetItems();
        }
        private async Task GetItems()
        {
            IsDone = true;
            var response = await itemService.GetAllItemsAsync(pageDto);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var content = await response.Content.ReadFromJsonAsync<List<ItemDto>>();
                if (content != null)
                {
                    items.AddRange(content);
                    if (content.Count < pageDto.pageSize)
                    {
                        IsDone = true;
                    }
                    else
                    {
                        IsDone = false;
                    }
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                IsDone = true;
                return;
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "حدث خطأ في تحميل المنتجات");
            }
        }
        public async ValueTask DisposeAsync()
        {
            await JSRuntime.InvokeVoidAsync("removeScrollListener");
            dotNetRef?.Dispose();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                dotNetRef = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync("registerScrollHandler", dotNetRef, nameof(OnPageChanged));
            }
        }
        [JSInvokable]
        public async Task OnPageChanged()
        {
            if(IsDone) return;
            pageDto.pageIndex++;
            await GetItems();
            StateHasChanged();
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
        private void AddSize(int id)
        {
            NavigationManager.NavigateTo($"/sizemanegment/{id}");
        }
        private async Task SerchItemByName()
        {
            if (string.IsNullOrWhiteSpace(Search))
            {
                await JSRuntime.InvokeVoidAsync("alartError", "الرجاء إدخال أسم للبحث");
                return;
            }
            var response = await itemService.SerchItem(Search);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                if (content != null)
                {
                    var Items = await content.ReadFromJsonAsync<List<ItemDto>>();
                    if (Items != null && Items.Count > 0)
                    {
                        items = Items;
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alartError", "لا يوجد منتجات بهذا الأسم");
                    }
                }
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", "حدث خطا في البحث عن المنتجات");
            }
        }
    }
}
