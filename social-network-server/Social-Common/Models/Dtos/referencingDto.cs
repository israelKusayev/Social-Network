using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Common.Models.Dtos
{
    public class ReferencingDto
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}
