namespace SherdProject.DTO
{
    public class BillsOut
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }
        public List<BillsItemDto>? BillsItems { get; set; }
    }
}
