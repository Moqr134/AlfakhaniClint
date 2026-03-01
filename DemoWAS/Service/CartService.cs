using SherdProject.DTO;

namespace DemoWAS.Service
{
    public class CartService
    {
        public List<CartDto> Items { get; private set; } = new();

        public void AddToCart(CartDto item)
        {
            var existing = Items.Find(i => i.Name == item.Name);
            if (existing != null)
            {
                existing.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public double TotalPrice => Items.Sum(i => i.Price * i.Quantity)+1000;
        public int Count => Items.Count;
    }
}
