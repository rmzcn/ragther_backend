using System;

namespace ragther.entity.ViewModels
{
    public class VMChatMessageGet
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public string authorUserName { get; set; }
        public bool isRead { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}