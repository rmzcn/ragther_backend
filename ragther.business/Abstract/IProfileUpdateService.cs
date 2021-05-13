using Microsoft.AspNetCore.Http;
using ragther.Core.Utilities.Results;

namespace ragther.business.Abstract
{
    public interface IProfileUpdateService
    {
        IResult ProfileImageUpload(string requesterUserName, IFormFile file);
        IResult UpdatePassword(string requesterUserName, string newPassword);
        IResult UpdateProfileDescription(string requesterUserName, string newDescription);
        IResult UpdateHiddenProfileDescription(string requesterUserName, string newHiddenDescription);
        IResult SetProfileVisibility(string requesterUserName, bool visible);
    }
}