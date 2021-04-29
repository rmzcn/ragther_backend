using System;
using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMNewTodoPost:IVModel
    {
        public int CreatorUserId { get; set; }
        public string imageUrl { get; set; }
        public string Description { get; set; }
        public DateTime UntilWhen { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
    }
}