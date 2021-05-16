using System;
using System.Collections.Generic;
using AutoMapper;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.Notice
{
    public class NoticeManager : INoticeService
    {
        ITodoRepository _todoRepository;
        IUserRepository _userRepository;
        IMapper _mapper;
        ICommentRepository _commentRepository;
        INoticeRepository _noticeRepository;
        public NoticeManager(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper, ICommentRepository commentRepository, INoticeRepository noticeRepository){
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _noticeRepository = noticeRepository;
            _mapper = mapper;
        }
        public IResult CreateNotice(int userIdFrom, int userIdTo, int noticeTypeId, string relevantIdOrUrl = null)
        {
            // TODO - Can use strategy pattern in here.
            // This funciton not contains any user check or todo check. 
            // They must be already checked before using this function.
            // if notice type is comment, like or remind relevantIdOrUrl must be todo id

            entity.Notice notice = new entity.Notice(){
                NoticeTypeId = noticeTypeId,
                RelevantUserId = userIdTo,
                OwnerUserId = userIdFrom,
                RelevantURL = relevantIdOrUrl,
                isRead = false,
                CreatedAt = DateTime.Now
            };
            _noticeRepository.Add(notice);
            return new SuccessResult();
        }

        public IResult DeleteAllNotices(string requesterUserName)
        {
            if (_userRepository.Get(u => u.UserName == requesterUserName) == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var notices = _noticeRepository.GetListByFilterOrAll(n => n.RelevantUser.UserName == requesterUserName);
            foreach (var notice in notices)
            {
                _noticeRepository.Delete(notice);
            }
            return new SuccessResult(Messages.DeletedAllNotices);
        }

        public IDataResult<List<VMNoticeGet>> GetNotices(string requesterUserName)
        {
            // TODO - Bu fonksiyonda bütün bildirimler geri gider. Burayı belirli sayıda bildirim gidecek şekilde yeniden düzenle

            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorDataResult<List<VMNoticeGet>>(Messages.UserNotFound);
            }
            var dbNotices = _noticeRepository.GetListByFilterOrAll( n => n.RelevantUser.UserName == requesterUserName);
            dbNotices.Sort((x, y) => DateTime.Compare(y.CreatedAt, x.CreatedAt));

            var notices = _mapper.Map<List<VMNoticeGet>>(dbNotices);
            return new SuccessDataResult<List<VMNoticeGet>>(notices);
        }

        public IResult ReadNotices(string requesterUserName)
        {
            if (_userRepository.Get(u => u.UserName == requesterUserName) == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var notices = _noticeRepository.GetListByFilterOrAll(n => n.RelevantUser.UserName == requesterUserName);
            foreach (var notice in notices)
            {
                notice.isRead=true;
                _noticeRepository.Update(notice);
            }
            return new SuccessResult(Messages.ReadedAllNotices);
        }
    }
}