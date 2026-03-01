using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Threading.Tasks;

namespace DemoWAS.Pages.MainPages
{
    public partial class Cart
    {
        [Inject] private IJSRuntime jS { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        private async Task NavigateToCheckOut()
        {
            if (CartService.Items.Count == 0)
            {
                await jS.InvokeVoidAsync("alartError", "السلة فارغة يرجى اضافه منتجات الى السلة لأكمال عملية الشراء");
                return;
            }
            NavigationManager.NavigateTo("/checkout");
        }
        private void IncrementQuantity(CartDto item)
        {
            if (item.Type == "weight")
                if(item.Quantity==0.125)
                    item.Quantity += 0.125;
                else
                    item.Quantity += 0.250;
            else
                item.Quantity++;
        }
        private void DecrementQuantity(CartDto item)
        {
            if (item.Type == "weight")
            {
                if (item.Quantity == 0.250)
                    item.Quantity -= 0.125;
                else
                    if (item.Quantity > 0.250)
                        item.Quantity -= 0.250;
                    else
                        CartService.Items.Remove(item);
            }
            else
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else if (item.Quantity == 1)
                {
                    CartService.Items.Remove(item);
                }
            }
        }
        private void RemoveItem(CartDto item)
        {
            CartService.Items.Remove(item);
        }
    }
}
