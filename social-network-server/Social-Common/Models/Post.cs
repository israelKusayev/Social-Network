using Social_Common.Enum;
using System;

namespace Social_Common.Models
{
    public class Post
    {
        public string PostId { get; set; }
        string ImgUrl { get; set; }
        string Content { get; set; }
        DateTime CreatedOn { get; set; }
        PostVisabilityOptions Visability { get; set; }
    }
}
