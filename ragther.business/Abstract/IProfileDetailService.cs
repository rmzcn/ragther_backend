using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface IProfileDetailService
    {
        IDataResult<entity.ProfileDetail> GetProfileDetailsByUserID(int userId);
        IResult IsHiddenProfile(string userName);
    }
}