using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SherdProject.DTO
{
    public class SizeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public int ItemId { get; set; }
        public int CreateBy { get; set; }
    }
}
