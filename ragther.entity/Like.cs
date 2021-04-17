using System;
using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class Like
    {
        public int TodoId { get; set; }
        public Todo Todo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime LikeDate { get; set; }
    }
}