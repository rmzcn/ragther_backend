using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.User
{
    public class UserManager : IUserService
    {
        IUserRepository _userRepository;
        IMapper _mapper;
        IFriendshipService _friendshipService;
        public UserManager(IUserRepository userRepository, IMapper mapper, IFriendshipService friendshipService){
            _userRepository = userRepository;
            _mapper = mapper;
            _friendshipService = friendshipService;
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
            var friendshipServiceData = _friendshipService.isFriends(userName,requesterUserName);
            if (friendshipServiceData.Success)
            {
                var result = _userRepository.GetUserProfile(userName);
                return new SuccessDataResult<VMUserProfileGet>(result);
            }
            else if (friendshipServiceData.Message == Messages.UsersAreNotFriends)
            {
                return new ErrorDataResult<VMUserProfileGet>(Messages.UsersAreNotFriends);
            }
            else
            {
                return new ErrorDataResult<VMUserProfileGet>(Messages.UserNotFound);
            }
            
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