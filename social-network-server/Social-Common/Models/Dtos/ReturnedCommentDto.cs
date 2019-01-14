using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Social_Common.Models.Dtos
{
    public class ReturnedCommentDto
    {
        [JsonProperty(PropertyName = "commentId")]
        public string CommentId { get; set; }
        [JsonProperty(PropertyName = "imgUrl")]
        public string ImgUrl { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "time")]
        public DateTime CreatedOn { get; set; }
        [JsonProperty(PropertyName = "User")]
        public User CreatedBy { get; set; }
        [JsonProperty(PropertyName = "referencing")]
        public List<User> Referencing { get; set; }
        [JsonProperty(PropertyName = "postId")]
        public string PostId { get; set; }
        [JsonProperty(PropertyName = "isLiked")]
        public bool IsLiked { get; set; }
        [JsonProperty(PropertyName = "numberOfLikes")]
        public int Likes { get; set; }
    }
}
