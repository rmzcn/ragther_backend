using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class TodoCondition
    {
        [Key]
        public int ConditionId { get; set; }
        public string Name { get; set; }

        //navigations
        public List<Todo> Todos { get; set; }
    }
}