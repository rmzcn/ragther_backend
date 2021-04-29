using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMUserLoginPost:IVModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}