using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class Notice
    {
        [Key]
        public int NoticeId { get; set; }
        public int NoticeTypeId { get; set; }
        public NoticeType NoticeType { get; set; }
        public int OwnerUserId { get; set; }
        public User OwnerUser { get; set; }
        public int RelevantUserId { get; set; }
        public User RelevantUser { get; set; }
        public string RelevantURL { get; set; }// to-do, profile, comment or etc.
        public bool isRead { get; set; }

    }
}