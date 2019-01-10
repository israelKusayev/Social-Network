using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Common.Models.Dtos
{
    public class ReturnedCommentDto : Comment
    {
        User CreatedBy { get; set; }
        List<User> Referencing { get; set; }
        string PostId { get; set; } 
    }
}
