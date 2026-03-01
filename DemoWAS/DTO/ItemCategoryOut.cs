namespace SherdProject.DTO
{
    public class ItemCategoryOut
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public List<ItemDto>? Items { get; set; }
    }
}
