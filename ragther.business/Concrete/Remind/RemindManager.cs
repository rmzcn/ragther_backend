using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;

namespace ragther.business.Concrete.Remind
{
    public class RemindManager : IRemindService
    {
        IUserRepository _userRepository;
        ITodoRepository _todoRepository;
        IRemindRepository _remindRepository;
        INoticeService _noticeService;
        IProfileDetailService _profileDetailService;

        public RemindManager(INoticeService noticeService, IRemindRepository remindRepository, ITodoRepository todoRepository, IUserRepository userRepository, IProfileDetailService profileDetailService)
        {
            _noticeService = noticeService;
            _remindRepository = remindRepository;
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _profileDetailService = profileDetailService;
        }
        public IResult Remind(int todoId, string requesterUserName)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            entity.Remind remindForCheck = _remindRepository.Get( lk => lk.UserId == user.UserId && lk.TodoId == todoId);
            
            if (remindForCheck != null)
            {
                return new ErrorResult(Messages.RemindedAlready);
            }

            entity.Remind remind = new entity.Remind(){
                RemindDate = System.DateTime.Now,
                TodoId = todoId,
                UserId = user.UserId
            };
            _remindRepository.Add(remind);

            //create notice
            _noticeService.CreateNotice(user.UserId,todo.CreatorUserId, NoticeTypes.Remind, todo.TodoId.ToString());

            //update todo's remind count
            todo.RemindCount += 1; 
            _todoRepository.Update(todo);

            //update users profile score
            _profileDetailService.SetProfileScore(user.UserId, Score.RemindScore);

             return new SuccessResult(Messages.Reminded);

        }
    }
}