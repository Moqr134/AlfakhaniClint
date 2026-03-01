namespace SherdProject.DTO
{
    public class BillsDto
    {
        public string Id { get; set; } = string.Empty;
        public double TotalAmount { get; set; }
        public string? Name {  get; set; }
        public string? PhoneNamper {  get; set; }
        public string? Location {  get; set; }
        public string? Description { get; set; }
        public List<BillsItemDto>? BillsItems { get; set; }
    }
}
