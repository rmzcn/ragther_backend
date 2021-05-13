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
        IUserRepository _userRepository;
        
        public ProfileDetailManager(IProfileDetailRepository profileDetailRepository, IUserRepository userRepository){
            _profileDetailRepository = profileDetailRepository;
            _userRepository = userRepository;
        }

        public IResult SetFriendCount(int userId, bool add = true)
        {
            var userProifle = _profileDetailRepository.Get( p => p.UserId == userId);
            if (userProifle == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            if (add)
            {
                userProifle.FriendCount ++;
            }
            else
            {
                userProifle.FriendCount --;
            }
            
            _profileDetailRepository.Update(userProifle);
            return new SuccessResult(Messages.FriendCountUpdated);
        }

        public IResult SetHelpCount(int userId, bool add = true)
        {
            var userProifle = _profileDetailRepository.Get( p => p.UserId == userId);
            if (userProifle == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (add)
            {
                userProifle.HelpCount ++;
            }
            else
            {
                userProifle.HelpCount --;
            }
            _profileDetailRepository.Update(userProifle);
            return new SuccessResult(Messages.HelpCountUpdated);
        }

        public IResult SetProfileScore(int userId, int score, bool add = true)
        {
            //score parametresi mevcut olan skorun üstüne eklenir yada çıkarılır
            var userProifle = _profileDetailRepository.Get( p => p.UserId == userId);
            if (userProifle == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (add)
            {
                userProifle.ProfileScore += score;
            }
            else
            {
                userProifle.ProfileScore -= score;
            }
            
            _profileDetailRepository.Update(userProifle);
            return new SuccessResult(Messages.ProfileScoreUpdated);
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
            if (user == null)
            {
                return new ErrorResult();
            }
            bool result = user.IsHiddenProfile;
            if (result)
            {
                //profile is hidden
                return new SuccessResult();
            }
            //profile is not hidden
            return new ErrorResult();
        }
    }
}