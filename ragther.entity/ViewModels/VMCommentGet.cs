using System;

namespace ragther.entity.ViewModels
{
    public class VMCommentGet
    {
        public int ID { get; set; }
        public int TodoId { get; set; }
        public VMInnerUserInfo userInfo { get; set; }
        public bool IsOffer { get; set; }
        public int offerStatus { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}