using Microsoft.AspNetCore.Http;
namespace DemoWAS.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public IFormFile? CategoryImage { get; set; }
    }
}
