
using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMUserLoggedinGet:IVModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string SessionToken { get; set; }
    }
}