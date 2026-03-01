namespace SherdProject.DTO
{
    public class BillsItemDto
    {
        public required string ItemName { get; set; }
        public required int ItemId { get; set; }
        public required double Quantity { get; set; } = 1;
        public required double Price { get; set; }
        public required double TotalPrice { get; set; }
    }
}
