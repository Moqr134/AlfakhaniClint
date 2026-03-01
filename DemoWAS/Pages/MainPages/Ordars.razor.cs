using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.MainPages
{
    public partial class Ordars
    {
        [Inject] private IBillsService billsService { get; set; } = default!;
        private List<BillsOut> bills = new List<BillsOut>();
        [Inject] private IJSRuntime jS { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            var Respnse = await billsService.GetAllMyBills();
            if (Respnse.IsSuccessStatusCode)
            {
                var content = await Respnse.Content.ReadFromJsonAsync<List<BillsOut>>();
                if (content != null)
                {
                    bills = content;
                }
            }
            else if (Respnse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await jS.InvokeVoidAsync("alartError", "ليس لديك صلاحية للوصول إلى هذه الصفحة");
            }
            else if (Respnse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                bills = new List<BillsOut>();
            }
            else
            {
                await jS.InvokeVoidAsync("alartError", "حدث خطا ما");
            }
        }
        public async Task CancleOrder(BillsOut billsOut)
        {
            var response = await billsService.CancleOrder(billsOut);
            if (response.IsSuccessStatusCode)
            {
                await jS.InvokeVoidAsync("alart", "تم إلغاء الطلب بنجاح");
                bills.Remove(billsOut);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await jS.InvokeVoidAsync("alartError", "ليس لديك صلاحية للوصول إلى هذه الصفحة");
            }
            else
            {
                await jS.InvokeVoidAsync("alartError", "حدث خطا ما");
            }
        }
    }
}
