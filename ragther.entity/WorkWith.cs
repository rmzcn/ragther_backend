using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class WorkWith
    {
        public int TodoId { get; set; }
        public Todo Todo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
} 