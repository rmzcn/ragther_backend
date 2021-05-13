using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;

namespace ragther.business.Concrete.Like
{
    public class LikeManager : ILikeService
    {
        ILikeRepository _likeRepository;
        IUserRepository _userRepository;
        INoticeService _noticeService;
        IProfileDetailService _profileDetailService;
        ITodoRepository _todoRepository;
        public LikeManager(ILikeRepository likeRepository, INoticeService noticeService, IUserRepository userRepository, ITodoRepository todoRepository, IProfileDetailService profileDetailService)
        {
            _likeRepository = likeRepository;
            _noticeService = noticeService;
            _userRepository = userRepository;
            _todoRepository = todoRepository; 
            _profileDetailService = profileDetailService;
        }
        public IResult Like(int todoId, string requesterUserName)
        {
            entity.User user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            entity.Todo todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }

            entity.Like likeForCheck = _likeRepository.Get( lk => lk.UserId == user.UserId && lk.TodoId == todoId);

            if (likeForCheck != null)
            {
                return new ErrorResult(Messages.LikedAlready);
            }

            entity.Like like = new entity.Like(){
                LikeDate = System.DateTime.Now,
                TodoId = todoId,
                UserId = user.UserId
            };
            _likeRepository.Add(like);
            //create notice
            _noticeService.CreateNotice(user.UserId,todo.CreatorUserId, NoticeTypes.Like, todo.TodoId.ToString());

            //update todo's like count
            todo.LikeCount += 1; 
            _todoRepository.Update(todo);

            //update users profile score
            _profileDetailService.SetProfileScore(user.UserId, Score.LikeScore);
            
            return new SuccessResult(Messages.Liked);
        }

        public IResult UnLike(int todoId, string requesterUserName)
        {
            entity.User user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            entity.Todo todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }

            entity.Like like = _likeRepository.Get( lk => lk.UserId == user.UserId && lk.TodoId == todoId);
            if (like != null && todo.LikeCount > 0)
            {
                //unlike
                todo.LikeCount -= 1; 
                _todoRepository.Update(todo);
                

                //update users profile score
                _profileDetailService.SetProfileScore(user.UserId, Score.UnLikeScore);

                //deleting like from todo
                _likeRepository.Delete(like);
                return new SuccessResult(Messages.UnLiked);
            }
            return new ErrorResult(Messages.UnLikedAlready);
        }
    }
}