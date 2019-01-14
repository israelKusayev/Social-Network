using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Common.Models.Dtos
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public string Image { get; set; }
        public string PostId { get; set; }
        public List<ReferencingDto> Referencing { get; set; }
    }
}
