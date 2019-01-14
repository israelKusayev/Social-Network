using Social_Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Common.Models.Dtos
{
    public class CreatePostDto
    {
        public string Image { get; set; }
        public string Content { get; set; }
        public PostVisabilityOptions WhoIsWatching { get; set; }
        public List<ReferencingDto> Referencing { get; set; }
    }
}
