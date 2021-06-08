using System;
using System.Collections.Generic;
using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMTodoGet:IVModel
    {
        public int TodoId { get; set; }
        public VMInnerUserInfo userInfo { get; set; }
        public List<string> tags { get; set; }
        public List<string> workWiths { get; set; }
        public string imageUrl { get; set; }
        public string Description { get; set; }
        public DateTime UntilWhen { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Address { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string TodoCondition { get; set; }
        public int LikeCount { get; set; }
        public int RemindCount { get; set; }
        public int CommentCount { get; set; }
    }
}