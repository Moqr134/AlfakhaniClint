using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class ManegarBills
    {
        [Inject]
        public IBillsService BillsService { get; set; } = default!;
        [Inject]
        public IItemService ItemService { get; set; } = default!;
        [Inject]
        public IJSRuntime jS { get; set; } = default!;
        private List<BillManegearOut> bills = new List<BillManegearOut>();
        private PageDto pageDto = new PageDto
        {
            pageIndex = 1,
            pageSize = 10
        };
        private DotNetObjectReference<ManegarBills>? dotNetRef;
        private bool IsDone { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            await LoadBillsAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                dotNetRef = DotNetObjectReference.Create(this);
                await jS.InvokeVoidAsync("registerScrollHandler",dotNetRef,nameof(OnPageChanged));
            }
        }
        public async ValueTask DisposeAsync()
        {
            await jS.InvokeVoidAsync("removeScrollListener");
            dotNetRef?.Dispose();
        }
        private async Task LoadBillsAsync()
        {
            var response = await BillsService.GetManegarBills(pageDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<BillManegearOut>>();
                if (result != null)
                {
                    bills.AddRange(result);
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                IsDone = true;
                return;
            }
            else
            {
                await jS.InvokeVoidAsync("alartError", "حدث خطا اثناء تحميل القوائم");
            }
        }
        [JSInvokable]
        public async Task OnPageChanged()
        {
            if (!IsDone)
            {
                pageDto.pageIndex += 1;
                await LoadBillsAsync();
                StateHasChanged();
            }
            return;
        }
    }
}
