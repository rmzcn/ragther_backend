using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMUserSearchResultGet:IVModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ProfileImageURL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}