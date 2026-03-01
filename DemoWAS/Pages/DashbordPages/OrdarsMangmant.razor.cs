using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class OrdarsMangmant
    {
        [Inject]
        public IBillsService BillsService { get; set; } = default!;
        private List<BillsAdminOut> BillsList { get; set; } = new List<BillsAdminOut>();
        private bool IsLoading { get; set; } = true;
        [Inject]
        private IJSRuntime jS { get; set; } = default!;
        private string? refuseReason = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await BillsService.GetBills();
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<List<BillsAdminOut>>();
                    if (content != null)
                    {
                        BillsList = content;
                    }
                    else
                    {
                        BillsList = new List<BillsAdminOut>();
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    BillsList = new List<BillsAdminOut>();
                }
                else
                {
                    await jS.InvokeVoidAsync("alartError", "حدث خطأ أثناء تحميل البيانات");
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
        private async Task ChangeStatus(BillsAdminOut bill)
        {
            var changeStatusDto = new ChangeStutesDto();
            changeStatusDto.Numper = bill.BillNumper;
            if (bill.Status == "Waiting")
            {
                changeStatusDto.Status = "InProgress";
            }
            else if (bill.Status == "InProgress")
            {
                changeStatusDto.Status = "Completed";
            }
            else if (bill.Status == "Completed")
            {
                changeStatusDto.Status = "Done";
            }
            else if (bill.Status == "Done")
            {
                await jS.InvokeVoidAsync("alartError", "لا يمكن تغيير الحالة");
                return;
            }
            var response = await BillsService.ChangeStatus(changeStatusDto);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                bill.Status = changeStatusDto.Status;
                await jS.InvokeVoidAsync("alart", responseContent);
            }
            else
            {
                await jS.InvokeVoidAsync("alartError", "حدث خطأ أثناء تغيير الحالة");
            }
        }
        private async Task OpenRefuseModal(int id)
        {
            refuseReason = string.Empty;
            await jS.InvokeVoidAsync("ShowItem", id);
        }
        private async Task RefuseOrder(BillsAdminOut bill)
        {
            if (string.IsNullOrEmpty(refuseReason))
            {
                await jS.InvokeVoidAsync("alartError", "يرجى إدخال سبب الرفض");
                return;
            }
            var changeStatusDto = new ChangeStutesDto
            {
                Numper = bill.BillNumper,
                Status = "Cancelled",
                RefuseReason = refuseReason
            };
            var response = await BillsService.ChangeStatus(changeStatusDto);
            if (response.IsSuccessStatusCode)
            {
                bill.Status = changeStatusDto.Status;
                bill.RefuseReason = changeStatusDto.RefuseReason;
                await jS.InvokeVoidAsync("alart", "تم رفض الطلب بنجاح");
            }
            else
            {
                await jS.InvokeVoidAsync("alartError", "حدث خطأ أثناء رفض الطلب");
            }
        }
    }
}
