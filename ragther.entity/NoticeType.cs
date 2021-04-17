using System.ComponentModel.DataAnnotations;

namespace ragther.entity
{
    public class NoticeType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
}