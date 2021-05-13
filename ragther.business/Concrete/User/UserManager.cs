using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.entity.ViewModels;
using ragther.data.MessagesForRelations;

namespace ragther.business.Concrete.User
{
    public class UserManager : IUserService
    {
        IUserRepository _userRepository;
        IMapper _mapper;
        IFriendshipService _friendshipService;
        IProfileDetailRepository _profileDetailRepository;
        INoticeService _noticeService;
        public UserManager(IUserRepository userRepository, IMapper mapper, IFriendshipService friendshipService, IProfileDetailRepository profileDetailRepository, INoticeService noticeService){
            _userRepository = userRepository;
            _mapper = mapper;
            _friendshipService = friendshipService;
            _profileDetailRepository = profileDetailRepository;
            _noticeService = noticeService;
        }

        public IResult Register(VMUserRegisterPost newUser)
        {
            try
            {
                if (!this.IsMailRegistered(newUser.Email).Success
                && !this.IsUserNameRegistered(newUser.UserName).Success)
                {
                    return new ErrorResult(Messages.UserNameAlreadyTaken+newUser.UserName + "\n" + Messages.EmailAlreadyTaken+newUser.Email);
                }

                if(!this.IsMailRegistered(newUser.Email).Success)
                {
                    return new ErrorResult(Messages.EmailAlreadyTaken+newUser.Email);  
                }
                else if (!this.IsUserNameRegistered(newUser.UserName).Success)
                {
                    return new ErrorResult(Messages.UserNameAlreadyTaken+newUser.UserName);
                }
                
                else
                {
                    entity.User user = _mapper.Map<entity.User>(newUser);
                    user.CreatedAt = DateTime.Now;
                    _userRepository.Add(user);
                    _profileDetailRepository.Add(new entity.ProfileDetail(){
                        UserId = user.UserId,
                        IsHiddenProfile = false,
                        ProfileDescription = "Merhabalar. Buralarda Yeniyim."
                    });
                    return new SuccessResult(Messages.UserAdded + user.UserName);
                }
            }
            catch (System.Exception)
            {
                return new ErrorResult();
            }
        }

        public IDataResult<VMUserProfileGet> GetUserProfile(string userName, string requesterUserName)
        {
            // TODO - requester user name yerine json web tokenler ile güvenlik sağlanabilir. Bu metodun yapısı bozulmadan token üzerinden username alınabilir. token alma işlemi controller üzerinden yapılırsa çok iyi bir yaklaşım olmaz ancak sistem neredeyse hiç bir değişiklik yapmadan token ile güvenlik sağlanabilir.
            
            // TODO - Userlar arkadaş değilse detail alanını null gönder
            // var friendshipServiceData = _friendshipService.isFriends(userName,requesterUserName);
            var targetUserModel = _userRepository.Get(u => u.UserName == userName);
            var requesterUserModel = _userRepository.Get(u => u.UserName == requesterUserName);
            if (requesterUserModel == null || targetUserModel == null)
            {
                return new ErrorDataResult<VMUserProfileGet>(Messages.UserNotFound);
            }
            // if (friendshipServiceData.Success || userName == requesterUserName)
            // {
                var result = _userRepository.GetUserProfile(userName);
                _noticeService.CreateNotice(requesterUserModel.UserId, targetUserModel.UserId, NoticeTypes.LookedProfile);
                return new SuccessDataResult<VMUserProfileGet>(result);
            // }
            // else if (friendshipServiceData.Message == Messages.UsersAreNotFriends)
            // {
            //     return new ErrorDataResult<VMUserProfileGet>(Messages.UsersAreNotFriends);
            // }
            // else
            // {
            //     
            // }
            
        }

        public IResult IsMailRegistered(string email)
        {
            var result = _userRepository.Get(
                u => u.Email == email
            );
            if (result == null )
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.EmailAlreadyTaken+email);
            
        }

        public IResult IsUserNameRegistered(string userName)
        {
            var result = _userRepository.Get(
                u => u.UserName == userName
            );
            if (result == null )
            {
                return new SuccessResult();         
            }
            return new ErrorResult(Messages.EmailAlreadyTaken+userName);
        }

        public IDataResult<VMUserLoggedinGet> Login(string userName, string password)
        {
            VMUserLoggedinGet result = _userRepository.Login(userName,password);
            if (result == null)
            {
                return new ErrorDataResult<VMUserLoggedinGet>(Messages.UserNameOrPasswordİncorrect);
            }
            return new SuccessDataResult<VMUserLoggedinGet>(result);
        }

        public IDataResult<List<VMUserSearchResultGet>> GetUsersBySearchFilterString(string filterString)
        {
            List<VMUserSearchResultGet> result = _userRepository.GetUsersBySearchFilterString(filterString);
            return new SuccessDataResult<List<VMUserSearchResultGet>>(result);
        }

    }
}