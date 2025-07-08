using Microsoft.AspNetCore.Components.Forms;
namespace DemoWAS.DTO
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public string? ItemImage { get; set; }
        public string? ImageContentType { get; set; }

    }
}
