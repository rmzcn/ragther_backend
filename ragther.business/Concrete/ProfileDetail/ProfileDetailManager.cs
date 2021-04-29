using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.ProfileDetail
{
    public class ProfileDetailManager : IProfileDetailService
    {
        IProfileDetailRepository _profileDetailRepository;
        public ProfileDetailManager(IProfileDetailRepository profileDetailRepository){
            _profileDetailRepository = profileDetailRepository;
        }

        public IDataResult<entity.ProfileDetail> GetProfileDetailsByUserID(int userId)
        {
            var profileDetail = _profileDetailRepository.Get(
                pd => pd.UserId == userId
            );
            if (profileDetail == null)
            {
                return new ErrorDataResult<entity.ProfileDetail>(Messages.ProfileDetailNotFound);
            }
            return new SuccessDataResult<entity.ProfileDetail>(profileDetail);
        }

        public IResult IsHiddenProfile(string userName)
        {
            var user = _profileDetailRepository.Get(u => u.User.UserName == userName);
            bool result = user.IsHiddenProfile;
            if (result)
            {
                //profile is hidden
                return new SuccessResult();
            }
            //profile is not hidden
            return new ErrorResult();
        }

        public IResult Update(entity.ProfileDetail entity)
        {
            throw new System.NotImplementedException();
        }
    }
}