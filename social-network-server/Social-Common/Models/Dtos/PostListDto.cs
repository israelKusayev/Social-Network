using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Common.Models.Dtos
{
    public class PostListDto
    {
        public List<ReturnedPostDto> Posts { get; set; }
        public int Page { get; set; }
    }
}
