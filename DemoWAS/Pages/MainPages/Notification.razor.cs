using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;

namespace DemoWAS.Pages.MainPages
{
    public partial class Notification
    {
        [Inject]
        public INotifSrvice NotifSrvice { get; set; } = default!;
        private List<NotifecationDto>? Notifecations { get; set; }
        [Inject] private IJSRuntime jSRuntime { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            Notifecations = await NotifSrvice.GetAll();
            if (Notifecations == null || Notifecations.Count == 0)
            {
                Notifecations = new List<NotifecationDto>();
            }
        }
        private async Task DeleteNotif(int id)
        {
            bool result = await NotifSrvice.DeleteNotif(id);
            if (result)
            {
                Notifecations?.RemoveAll(n => n.Id == id);
                await jSRuntime.InvokeVoidAsync("alart", "تم حذف الإشعار بنجاح");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("alartError", "حدث خطأ أثناء حذف الإشعار");
            }
        }
    }
}
