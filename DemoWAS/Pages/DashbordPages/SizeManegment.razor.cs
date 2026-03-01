using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Threading.Tasks;

namespace DemoWAS.Pages.DashbordPages
{
    public partial class SizeManegment
    {
        [Parameter] public int id { get; set; }
        [Inject] private ISizeService sizeService { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private IJSRuntime js { get; set; } = default!;
        public SizeDto SizeDto { get; set; } = new SizeDto();
        private async Task AddNewSize()
        {
            if (string.IsNullOrEmpty(SizeDto?.Name) || SizeDto.Price <= 0 || id <= 0)
            {
                await js.InvokeVoidAsync("alartError", "الرجاء ملئ جميع الحقول");
                return;
            }
            SizeDto.ItemId = id;
            var requst = await sizeService.AddSize(SizeDto);
            if (requst.IsSuccessStatusCode)
            {
                await js.InvokeVoidAsync("alart", "تمت الاضافة بنجاح");
                navigationManager.NavigateTo("/menumanagement");
            }
            else
            {
                string errorMessage = await requst.Content.ReadAsStringAsync();
                await js.InvokeVoidAsync("alartError", errorMessage);
            }
        }
        private async Task DeleteSize()
        {
            if (string.IsNullOrEmpty(SizeDto.Name))
            {
                await js.InvokeVoidAsync("alartError", "الرجاء اختيار الحجم المراد حذفه");
                return;
            }
            SizeDto.ItemId = id;
            var requst = await sizeService.DeleteSize(SizeDto);
            if (requst.IsSuccessStatusCode)
            {
                await js.InvokeVoidAsync("alart", "تم الحذف بنجاح");
                navigationManager.NavigateTo("/menumanagement");
            }
            else
            {
                string errorMessage = await requst.Content.ReadAsStringAsync();
                await js.InvokeVoidAsync("alartError", errorMessage);
            }
        }
    }
}
