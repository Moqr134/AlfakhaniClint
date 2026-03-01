using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SherdProject.DTO
{
    public class NotifecationDto
    {
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Stutes { get; set; } 
        public string? RefuseReason { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
