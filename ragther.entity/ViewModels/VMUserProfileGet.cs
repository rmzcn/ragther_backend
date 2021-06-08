using System;
using ragther.entity;
using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMUserProfileGet:IVModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageURL { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public VMUserProfileDetailGet ProfileDetail { get; set; }
        
    }
}