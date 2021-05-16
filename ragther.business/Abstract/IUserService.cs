using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface IUserService
    {
        IDataResult<VMUserLoggedinGet> Login(string userName, string password);
        IDataResult<VMUserProfileGet> GetUserProfile(string userName,string requesterUserName);
        IResult Register(VMUserRegisterPost newUser);
        IDataResult<List<VMUserSearchResultGet>> GetUsersBySearchFilterString(string filterString);
        IResult IsMailRegistered(string email);
        IResult IsUserNameRegistered(string userName);
        IResult ForgetPassword(string requesterUserName);
        IResult SetPassword(string requesterUserName, string oldPassword, string newPassword);
        IResult GetPassword(string requesterUserName);
    }
}