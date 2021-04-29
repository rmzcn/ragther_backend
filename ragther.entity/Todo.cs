using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }
        public int CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public string imageUrl { get; set; }
        public string Description { get; set; }
        public DateTime UntilWhen { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public int TodoConditionId { get; set; }
        public TodoCondition TodoCondition { get; set; }
        public int LikeCount { get; set; }
        public int RemindCount { get; set; }
        public int CommentCount { get; set; }

        //navigations
        public List<TodoTag> TodoTags { get; set; }
        public List<WorkWith> WorkWiths { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public List<Remind> Reminds { get; set; }

    }
}