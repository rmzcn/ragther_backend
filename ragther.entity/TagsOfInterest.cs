using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class TagsOfInterest
    {
        [Key]
        public int TagsOfInterestId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}