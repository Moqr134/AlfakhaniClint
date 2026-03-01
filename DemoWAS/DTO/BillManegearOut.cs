using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SherdProject.DTO
{
    public class BillManegearOut
    {
        public int Id { get; set; }
        public string? BillNumper { get; set; }
        public double? TotalAmount { get; set; }
        public required string Status { get; set; }
        public required string Name { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public string? RefuseReason { get; set; } = string.Empty;
        public required DateTime Date { get; set; }

    }
}
