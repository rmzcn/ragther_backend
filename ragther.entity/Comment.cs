using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ragther.entity
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        [InverseProperty("ParentComment")]
        public List<Comment> Comments { get; set; }
        public int TodoId { get; set; }
        public Todo Todo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsOffer { get; set; }
        public int offerStatus { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}