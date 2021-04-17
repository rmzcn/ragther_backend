using System;

namespace ragther.entity
{
    public class Remind
    {
        public int TodoId { get; set; }
        public Todo Todo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime RemindDate { get; set; }
    }
}