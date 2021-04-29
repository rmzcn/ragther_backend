using System.Collections.Generic;
using System.Linq;
using ragther.data.Abstract;
using ragther.entity;
using ragther.core.DataAccess.EFCore;
using ragther.entity.ViewModels;
using Microsoft.EntityFrameworkCore;
using ragther.data.MessagesForRelations;
using AutoMapper;

namespace ragther.data.Concrete.EFCore
{
    public class EFCoreTodoRepository : EFCoreGenericRepository<Todo, RagtherDbContext>, ITodoRepository
    {
        IMapper _mapper;
        public EFCoreTodoRepository(IMapper mapper){
            _mapper = mapper;
        }
        public List<VMTodoGet> GetMainPageTodos(string requesterUserName, int pageNumber, int batchSize = 15)
        {
            //TODO - Burada batch size parametresine göre todo getir. Şuanda tablodaki tüm veriler üzerinde işlem yapılıyor. Verimsiz bir yöntem.
            using (var db = new RagtherDbContext())
            {
                List<VMTodoGet> result = db.Todos
                    .Where(t => t.CreatorUser.SendedRequests
                        .Where(r => r.FriendshipCondition.ConditionId == FriendshipAndFriendshipCondition.Friend && r.RecipientUser.UserName == requesterUserName)
                        .Any()
                        ||
                        t.CreatorUser.ReceivedRequests
                        .Where(r => r.FriendshipCondition.ConditionId == FriendshipAndFriendshipCondition.Friend && r.SenderUser.UserName == requesterUserName)
                        .Any()
                        || 
                        t.CreatorUser.UserName == requesterUserName)
                    .Select(t => new VMTodoGet{
                        TodoId = t.TodoId,
                        imageUrl = t.imageUrl,
                        Description = t.Description,
                        UntilWhen = t.UntilWhen,
                        CreatedAt = t.CreatedAt,
                        LocationLatitude = t.LocationLatitude,
                        LocationLongitude = t.LocationLongitude,
                        TodoCondition = t.TodoCondition.Name,
                        LikeCount = t.LikeCount,
                        RemindCount = t.RemindCount,
                        CommentCount = t.CommentCount,
                        userInfo = _mapper.Map<VMInnerUserInfo>(t.CreatorUser)
                    })
                    .ToList();
                return result;
            }
        }
    }
}