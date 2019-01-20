using System;
using System.Collections.Generic;
using System.Text;

namespace Notification_Common.Models.Dtos
{
    public class CommentActionDto
    {
        public User User { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
        public string ReciverId { get; set; }

        public int ActionId { get; set; }

    }
}
