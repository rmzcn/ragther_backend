using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMUserRegisterPost:IVModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

// {
//     "UserName": "admin",
//     "FirstName": "Ramazan Can",
//     "LastName": "Gölgen",
//     "Email": "ra@ragther.com",
//     "Password": "123456"
// }