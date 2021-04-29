using System.Collections.Generic;
using ragther.entity;
using ragther.core.DataAccess;
using ragther.entity.ViewModels;

namespace ragther.data.Abstract
{
    public interface IUserRepository: IRepository<User>
    {
        VMUserLoggedinGet Login(string userName, string password);
        VMUserProfileGet GetUserProfile(string userName);
        List<VMUserSearchResultGet> GetUsersBySearchFilterString(string filterString);
    }
}