using Newtonsoft.Json;
using Social_Common.Enum;
using System;

namespace Social_Common.Models
{
    public class Post
    {
        public string PostId { get; set; }
        public string ImgUrl { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public PostVisabilityOptions Visability { get; set; }
    }
}
