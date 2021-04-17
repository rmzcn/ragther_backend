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
        public DateTime UntilWhen { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Location { get; set; }//can be coordinates or address
        public int TodoConditionId { get; set; }
        public TodoCondition TodoCondition { get; set; }
        public int LikeCount { get; set; }

        //navigations
        public List<TodoTag> TodoTags { get; set; }
        public List<WorkWith> WorkWiths { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public List<Remind> Reminds { get; set; }

    }
}