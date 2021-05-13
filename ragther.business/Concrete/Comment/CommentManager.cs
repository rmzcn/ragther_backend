using ragther.business.Abstract;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.Comment
{
    public class CommentManager:ICommentService
    {
        ITodoRepository _todoRepository;
        IUserRepository _userRepository;
        IProfileDetailRepository _profileDetailRepository;
        IMapper _mapper;
        ICommentRepository _commentRepository;
        INoticeService _noticeService;
        IWorkWithService _workWithService;
        public CommentManager(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper, ICommentRepository commentRepository, INoticeService noticeService, IProfileDetailRepository profileDetailRepository, IWorkWithService workWithService){
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _profileDetailRepository = profileDetailRepository;
            _mapper = mapper;
            _noticeService = noticeService;
            _workWithService = workWithService;
        }

        public IResult AcceptOffer(int commentID, string requesterUserName)
        {
            entity.User user = _userRepository.Get( u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            entity.Comment comment = _commentRepository.Get(c => c.ID == commentID);
            if (comment == null)
            {
                return new ErrorResult(Messages.CommentNotFound);
            } 
            if (comment.IsOffer && comment.offerStatus == OfferStatus.Waiting)
            {
                entity.User commentCreatorUser = _userRepository.Get( u => u.UserId == comment.UserId); 
                comment.offerStatus = OfferStatus.Accepted;
                _commentRepository.Update(comment);

                //adding worker to todo
                _workWithService.AddWorker(comment.TodoId, commentCreatorUser.UserName);
                
                //creating notice
                _noticeService.CreateNotice(user.UserId, commentCreatorUser.UserId, NoticeTypes.OfferAccepted);
            }
            return new ErrorResult(Messages.CommentIsNotOffer);
        }

        public IResult RejectOffer(int commentID, string requesterUserName)
        {
            entity.User user = _userRepository.Get( u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            entity.Comment comment = _commentRepository.Get(c => c.ID == commentID);
            if (comment == null)
            {
                return new ErrorResult(Messages.CommentNotFound);
            } 
            if (comment.IsOffer && comment.offerStatus == OfferStatus.Waiting)
            {
                entity.User commentCreatorUser = _userRepository.Get( u => u.UserId == comment.UserId); 
                comment.offerStatus = OfferStatus.Rejected;
                _commentRepository.Update(comment);
                
                //creating notice
                _noticeService.CreateNotice(user.UserId, commentCreatorUser.UserId, NoticeTypes.OfferRejected);
            }
            return new ErrorResult(Messages.CommentIsNotOffer);
        }

        //WORKING
        public IResult Create(VMNewCommentPost newCommentModel)
        {
            var todo = _todoRepository.Get(t => t.TodoId == newCommentModel.TodoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            var user = _userRepository.Get(u => u.UserId == newCommentModel.UserId );
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            entity.Comment comment = _mapper.Map<entity.Comment>(newCommentModel);
            int todoOwnerId = todo.CreatorUserId;
            comment.CreatedAt = DateTime.Now;

            //create offer status if offer exist
            if (newCommentModel.IsOffer)
            {
                comment.offerStatus = OfferStatus.Waiting;
            }
            comment.offerStatus = OfferStatus.NoOffer;
            _commentRepository.Add(comment);

            //creating notice
            _noticeService.CreateNotice(newCommentModel.UserId, todoOwnerId, NoticeTypes.Comment, newCommentModel.TodoId.ToString());
            //updating todos comment count
            todo.CommentCount ++;

            //updating profile score
            var authorUser = _profileDetailRepository.Get( u => newCommentModel.UserId == u.UserId);
            authorUser.ProfileScore += Score.CommentScore;
            _profileDetailRepository.Update(authorUser);

            //yukarıdaki güncelleme işlemi profile detail manager üzerindeki metodlar ile de yapılabilir

            _todoRepository.Update(todo);
            return new SuccessResult(Messages.CommentCreated);        
        }

        public IResult Delete(int commentID, string requesterUserName)
        {
            var comment = _commentRepository.Get(c => c.ID == commentID && c.User.UserName == requesterUserName);
            if (comment == null)
            {
                return new ErrorResult(Messages.CommentNotFound);
            }
            _commentRepository.Delete(comment);
            return new SuccessResult(Messages.CommentDeleted);
        }

        //WORKING
        public IDataResult<List<VMCommentGet>> GetTodoComments(int todoID)
        {
            var todo = _todoRepository.Get(t => t.TodoId == todoID);
            if (todo == null)
            {
                return new ErrorDataResult<List<VMCommentGet>>(Messages.TodoNotFound);
            }
            var user = _userRepository.Get(u => u.UserId == todo.CreatorUserId);
            if (user == null)
            {
                return new ErrorDataResult<List<VMCommentGet>>(Messages.UserNotFound);
            }
            
            var comments = _commentRepository.GetListByFilterOrAll(c => c.TodoId == todoID);
            List<VMCommentGet> result = _mapper.Map<List<VMCommentGet>>(comments);
            foreach (var comment in comments)
            {
                comment.User = _userRepository.Get(u => u.UserId == comment.UserId);
            }
            foreach (var com in result)
            {
                com.userInfo =  _mapper.Map<VMInnerUserInfo>(comments.Where(c => c.TodoId == com.TodoId).FirstOrDefault().User);
            }
            return new SuccessDataResult<List<VMCommentGet>>(result);
        }

        
    }
}