namespace DemoWAS.DTO
{
    public class CartDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; } = 1;
        public int TotalPrice => Price * Quantity;
    }
}
