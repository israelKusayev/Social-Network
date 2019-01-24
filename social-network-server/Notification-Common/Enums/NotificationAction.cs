using System;
using System.Collections.Generic;
using System.Text;

namespace Notification_Common.Enums
{
    public enum NotificationAction
    {
        LikePost = 0,
        LikeComment = 1,
        CommentedOnPost = 2,
        MentionedInPost = 3,
        MentionedInComment = 4,
        Followed = 5,
        FollowRecomendation = 6
    }
}
