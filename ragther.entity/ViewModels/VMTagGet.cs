using System;

namespace ragther.entity.ViewModels
{
    public class VMTagGet
    {
        public int TagId { get; set; }
        public string Name { get; set; }         
        public string CreatorUserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}