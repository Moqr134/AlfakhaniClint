using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;

namespace DemoWAS.Pages.MainPages
{
    public partial class CheckOut
    {
        private BillsDto billsDto { get; set; } = new();
        [Inject] private IBillsService BillsService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        private List<BillsItemDto> billsItem { get; set; } = new();
        [Inject] IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] private CartService CartService { get; set; } = default!;
        string dviceToken = string.Empty;
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                dviceToken = await JSRuntime.InvokeAsync<string>("Fcm.requestAndGetToken");
                if (!string.IsNullOrEmpty(dviceToken))
                {
                    billsDto.Id = dviceToken;
                }
                else
                {
                    billsDto.Id = string.Empty;
                }
            }
        }
        private async Task AddBills()
        {
            if (string.IsNullOrEmpty(billsDto.Name))
            {
                await JSRuntime.InvokeVoidAsync("alartError", "يرجى ادخال الاسم");
                return;
            }
            else if (string.IsNullOrEmpty(billsDto.PhoneNamper))
                    {
                        await JSRuntime.InvokeVoidAsync("alartError", "يرجى ادخال رقم الهاتف");
                        return;
                    }
            else if (string.IsNullOrEmpty(billsDto.Location))
                    {
                        await JSRuntime.InvokeVoidAsync("alartError", "يرجى ادخال العنوان");
                        return;
            }
            foreach (var item in CartService.Items)
                {
                    billsItem.Add(new BillsItemDto
                    {
                        ItemName = item.Name,
                        ItemId = item.Id,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        TotalPrice = item.TotalPrice
                    });
                }
            billsDto.TotalAmount = CartService.TotalPrice;
            billsDto.BillsItems = billsItem;
            var response = await BillsService.AddBills(billsDto);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                CartService.Items.Clear();
                await JSRuntime.InvokeVoidAsync("alart", responseContent);
                NavigationManager.NavigateTo("/thank");
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alartError", responseContent);
            }

        }
    }
}
