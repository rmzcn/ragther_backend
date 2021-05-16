using System;

namespace ragther.entity.ViewModels
{
    public class VMNoticeGet
    {
        public int NoticeId { get; set; }
        public int OwnerUserName { get; set; }
        public int RelevantUserName { get; set; }
        public bool isRead { get; set; }
        public int NoticeTypeId { get; set; }
        public string RelevantURL { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}