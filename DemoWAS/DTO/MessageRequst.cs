using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SherdProject.DTO
{
    public class MessageRequst
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
        public required string DeviceToken { get; set; }
    }
}
