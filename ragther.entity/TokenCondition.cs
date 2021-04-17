using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class TokenCondition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        //navigations
        public List<MailUpdate> MailUpdates { get; set; }
    }
}