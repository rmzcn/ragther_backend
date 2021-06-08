using System;
using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public bool isRead { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // navigation
        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}