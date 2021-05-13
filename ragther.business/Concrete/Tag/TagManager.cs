using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.Tag
{
    public class TagManager:ITagService
    {
        ITodoRepository _todoRepository;
        IUserRepository _userRepository;
        IMapper _mapper;
        ITagRepository _tagRepository;
        public TagManager(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper, ITagRepository tagRepository){
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public IResult CreateTag(VMNewTagPost model, string requesterUserName)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName );
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (_tagRepository.Get(t => t.Name.ToLower() == model.Name.ToLower()) == null)
            {
                return new ErrorResult(Messages.TagAlreadyExists);
            }
            var tag = new entity.Tag(){
                Name = model.Name,
                CreatorUserId = user.UserId,
                CreatedAt = DateTime.Now
            };
            _tagRepository.Add(tag);
            return new SuccessResult(Messages.TagAdded);
        }

        public IDataResult<List<VMTagGet>> GetTagsByFilter(string filter, string requesterUserName)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName );
            if (user == null)
            {
                return new ErrorDataResult<List<VMTagGet>>(Messages.UserNotFound);
            }
            var tagList = _tagRepository.GetListByFilterOrAll(t => t.Name.Contains(filter));
            List<VMTagGet> mappedTags = _mapper.Map<List<VMTagGet>>(tagList);
            foreach (var tag in tagList)
            {
                tag.CreatorUser = _userRepository.Get(u => u.UserId == tag.CreatorUserId);
            }
            foreach (var mappedTag in mappedTags)
            {
                mappedTag.CreatorUserName = tagList.Where(t => t.TagId == mappedTag.TagId).FirstOrDefault().CreatorUser.UserName;
            }
            return new SuccessDataResult<List<VMTagGet>>(mappedTags);
        }
    }
}