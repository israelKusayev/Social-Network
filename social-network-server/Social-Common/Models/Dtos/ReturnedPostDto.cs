using System.Collections.Generic;

namespace Social_Common.Models.Dtos
{
    public class ReturnedPostDto : Post
    {
        User CreatedBy { get; set; }
        // List<User> Referencing { get; set; }
        public bool IsLiked { get; set; }
        public int Likes { get; set; }
    }
}
