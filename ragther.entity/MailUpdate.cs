using System;
using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class MailUpdate
    {
        [Key]
        public int MailUpdateId { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string MailWillUpdate { get; set; }
        public int TokenConditionId { get; set; }
        public TokenCondition TokenCondition { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}