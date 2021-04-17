using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ragther.entity
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public string Name { get; set; }         
        public int CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public DateTime CreatedAt { get; set; }

        //navigations
        public List<TagsOfInterest> TagsOfInterests { get; set; }
        public List<TodoTag> TodoTags { get; set; }
    }
}