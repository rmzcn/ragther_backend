using System;

namespace ragther.entity.ViewModels
{
    public class VMTodoUpdatePost
    {
        public int TodoId { get; set; }
        public string imageUrl { get; set; }
        public string Description { get; set; }
        public DateTime UntilWhen { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
    }
}