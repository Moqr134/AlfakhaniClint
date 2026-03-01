namespace SherdProject.DTO
{
    public class CartDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; } = 1;
        public string? Type { get; set; }
        public double TotalPrice => Price * Quantity;
    }
}
