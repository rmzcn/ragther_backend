using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public int FirstUserId { get; set; }
        public User FirstUser { get; set; }
        public int SecondUserId { get; set; }
        public User SecondUser { get; set; }

        // navigation
        public List<Message> Messages { get; set; }
    }
}