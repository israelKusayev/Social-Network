using System;

namespace Social_Common.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string ImgUrl { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
