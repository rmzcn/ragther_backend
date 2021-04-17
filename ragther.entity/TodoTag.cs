using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class TodoTag
    {
        public int TodoId { get; set; }
        public Todo Todo { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}