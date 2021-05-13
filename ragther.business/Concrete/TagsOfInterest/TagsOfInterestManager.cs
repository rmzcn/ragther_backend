using System.Collections.Generic;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.TagsOfInterest
{
    public class TagsOfInterestManager : ITagsOfInterestService
    {
        ITagsOfInterestRepository _tagsOfInterestRepository;
        ITagRepository _tagRepository;
        IUserRepository _userRepository;
        public TagsOfInterestManager(ITagsOfInterestRepository tagsOfInterestRepository, ITagRepository tagRepository, IUserRepository userRepository)
        {
            _tagsOfInterestRepository = tagsOfInterestRepository;
            _tagRepository = tagRepository;
            _userRepository = userRepository;
        }
        public IDataResult<List<VMTagGet>> GetInterestedTags(string requesterUserName)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorDataResult<List<VMTagGet>>(Messages.UserNotFound);
            }

            List<VMTagGet> result = new List<VMTagGet>();
            entity.Tag tag;
            entity.User creatorUser;
            var interestedTags = _tagsOfInterestRepository.GetListByFilterOrAll( it => it.UserId == user.UserId);
            if (interestedTags != null)
            {
                foreach (var interestedTag in interestedTags)
                {
                    tag = _tagRepository.Get(t => t.TagId == interestedTag.TagId);
                    creatorUser = _userRepository.Get(u => u.UserId == tag.CreatorUserId);
                    result.Add(new VMTagGet()
                    {
                        CreatedAt = tag.CreatedAt,
                        CreatorUserName = creatorUser.UserName,
                        Name = tag.Name,
                        TagId = tag.TagId
                    });
                }
                return new SuccessDataResult<List<VMTagGet>>(result);
            }
            return new ErrorDataResult<List<VMTagGet>>(Messages.InterestedTagsNotFound);
            
        }

        public IResult SetInterestedTags(string requesterUserName, List<int> tagIdList)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorDataResult<List<VMTagGet>>(Messages.UserNotFound);
            }

            var interestedTags = _tagsOfInterestRepository.GetListByFilterOrAll( it => it.UserId == user.UserId);

            entity.TagsOfInterest interestedTag;
            foreach (var tagId in tagIdList)
            {
                //gereksiz db trafiği. burayı düzelt
                interestedTag = _tagsOfInterestRepository.Get(it => it.TagId == tagId && it.UserId == user.UserId);
                if (interestedTag == null)
                {
                    _tagsOfInterestRepository.Add(new entity.TagsOfInterest(){
                        TagId = tagId,
                        UserId = user.UserId
                    });
                }
            }

            foreach (var tag in interestedTags)
            {
                if (!tagIdList.Contains(tag.TagId))
                {
                    _tagsOfInterestRepository.Delete(new entity.TagsOfInterest(){
                        TagId = tag.TagId,
                        UserId = user.UserId
                    });
                }
            }
            return new SuccessResult(Messages.TagsOfInterestUpdated);
            
        }
    }
}