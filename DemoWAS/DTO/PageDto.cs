using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SherdProject.DTO
{
    public class PageDto
    {
         public int pageIndex { get; set; } = 1;
         public int pageSize { get; set; } = 10;
    }
}
