using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ragther.entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(22, ErrorMessage="Max lenght is 22 char")]
        [MinLength(2, ErrorMessage="Min lenght is 2 char")]
        [Required(ErrorMessage="Name attribute is required")]
        public string UserName { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }

        //navigations

        [InverseProperty("CreatorUser")]
        public List<Tag> CreatedTags { get; set; }

        [InverseProperty("SenderUser")]
        public List<Friendship> SendedRequests { get; set; }

        [InverseProperty("RecipientUser")]
        public List<Friendship> ReceivedRequests { get; set; }
        public List<TagsOfInterest> TagsOfInterests { get; set; }
        public ProfileDetail ProfileDetail { get; set; }

        [InverseProperty("CreatorUser")]
        public List<Todo> Todos { get; set; }
        public List<WorkWith> WorkWiths { get; set; }
        public List<MailUpdate> MailUpdates   { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public List<Remind> Reminds { get; set; }

        [InverseProperty("OwnerUser")]
        public List<Notice> OwnNotices { get; set; }

        [InverseProperty("RelevantUser")]
        public List<Notice> RelevantNotices { get; set; }
        
    }
}