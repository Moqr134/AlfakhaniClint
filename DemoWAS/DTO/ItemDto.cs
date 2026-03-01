namespace SherdProject.DTO
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? Type { get; set; }
        public double Price { get; set; }
        public bool Shwoing { get; set; } = true;
        public string? ItemImage { get; set; }
        public string? ImageContentType { get; set; }
        public List<SizeDto>? Sizes { get; set; }
    }
}
