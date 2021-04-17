using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class FriendshipCondition
    {
        [Key]
        public int ConditionId { get; set; }
        public string Description { get; set; }

        //navigations
        public List<Friendship> Friendships { get; set; }
    }
}