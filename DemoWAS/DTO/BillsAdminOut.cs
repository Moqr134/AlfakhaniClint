using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SherdProject.DTO
{
    public class BillsAdminOut
    {
        public int Id { get; set; }
        public string? BillNumper { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; } = "Waiting";
        public required string Name { get; set; }
        public required string PhoneNamper { get; set; }
        public required string Location { get; set; }
        public string? Description { get; set; }
        public string? RefuseReason { get; set; } = string.Empty;
        public required DateTime Date { get; set; }
        public required List<BillsItemDto> BillsItems { get; set; }
    }
}
