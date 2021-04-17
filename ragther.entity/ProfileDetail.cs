using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class ProfileDetail
    {
        [Key]
        public int ProfileDetailId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        //will be changed this annotation
        [MaxLength(400)]
        public string ProfileDescription { get; set; }
        public string HiddenProfileDescription { get; set; }
        public bool IsHiddenProfile { get; set; }
        public int ProfileScore { get; set; }
        public int FriendCount { get; set; }
        public int HelpCount { get; set; }
        
    }
}