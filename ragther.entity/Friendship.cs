using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ragther.entity
{
    public class Friendship
    {
        [Key]
        public int FriendshipKey { get; set; }
        public int SenderUserId { get; set; }
        public User SenderUser { get; set; }
        public int RecipientUserId { get; set; }
        public User RecipientUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FriendshipConditionId { get; set; }
        public FriendshipCondition FriendshipCondition { get; set; }
    }
}