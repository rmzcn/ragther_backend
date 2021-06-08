using System;

namespace ragther.entity.ViewModels
{
    public class VMNoticeGet
    {
        public int NoticeId { get; set; }
        public string OwnerUserName { get; set; }
        public string RelevantUserName { get; set; }
        public bool isRead { get; set; }
        public int NoticeTypeId { get; set; }
        public string RelevantURL { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}