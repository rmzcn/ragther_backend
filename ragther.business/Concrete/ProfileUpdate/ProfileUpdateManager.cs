using System;
using System.IO;
using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;

namespace ragther.business.Concrete.ProfileUpdate
{
    public class ProfileUpdateManager : IProfileUpdateService
    {
        IUserRepository _userRepository;
        IMapper _mapper;
        IProfileDetailRepository _profileDetailRepository;
        public ProfileUpdateManager(IUserRepository userRepository, IMapper mapper, IProfileDetailRepository profileDetailRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _profileDetailRepository = profileDetailRepository;
        }
        public IResult ProfileImageUpload(string requesterUserName, IFormFile file)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            try
            {
                var folderName = Path.Combine("Resources","Images","ProfileImages");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                   var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    user.ProfileImageURL = dbPath;
                    _userRepository.Update(user);
                    return new SuccessResult(Messages.FileUploaded);
                }
                return new ErrorResult(Messages.FileLengthZero);
            }
            catch (System.Exception ex)
            {
               return new ErrorResult(ex.Message);
            }
        }

        public IResult SetProfileVisibility(string requesterUserName, bool visible)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var profileDetail = _profileDetailRepository.Get( pd => pd.UserId == user.UserId);
            profileDetail.IsHiddenProfile = !visible;
            _profileDetailRepository.Update(profileDetail);

            string resultMessage;
            if (visible)
            {
                resultMessage = Messages.ProfileVisibilityUpdatedToVisible;
            }
            else
            {
                resultMessage = Messages.ProfileVisibilityUpdatedToHidden;
            }
            return new SuccessResult(resultMessage);
        }

        public IResult UpdateHiddenProfileDescription(string requesterUserName, string newHiddenDescription)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var profileDetail = _profileDetailRepository.Get( pd => pd.UserId == user.UserId);
            profileDetail.HiddenProfileDescription = newHiddenDescription;
            _profileDetailRepository.Update(profileDetail);

            return new SuccessResult(Messages.HiddenProfileDescriptionUpdated);
        }


        //DANGER--->DONT USE THIS FUNCTION FOR PASSWORD UPDATING
        //USER MANAGER CLASS USES FOR PASSWORD UPDATING
        public IResult UpdatePassword(string requesterUserName, string newPassword)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            user.Password = newPassword;
            _userRepository.Update(user);
            return new SuccessResult(Messages.PasswordUpdated);
        }

        public IResult UpdateProfileDescription(string requesterUserName, string newDescription)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var profileDetail = _profileDetailRepository.Get( pd => pd.UserId == user.UserId);
            profileDetail.ProfileDescription = newDescription;
            _profileDetailRepository.Update(profileDetail);

            return new SuccessResult(Messages.ProfileDescriptionUpdated);
        }
    }
}