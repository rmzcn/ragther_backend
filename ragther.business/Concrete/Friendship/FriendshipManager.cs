using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;

namespace ragther.business.Concrete.Friendship
{
    public class FriendshipManager : IFriendshipService
    {
        IUserRepository _userRepository;
        IFriendshipRepository _friendshipRepository;

        //TODO - Arkadaş ekleme yapılırken: gönderici id ve alıcı id kontrol edilsin ilk olarak. Gönderici ve alıcı id(SIRASI FARK ETMEZ) bir arkadaşlığı temsil eder sonuçta

        public FriendshipManager(IUserRepository userRepository, IFriendshipRepository friendshipRepository){
            _userRepository = userRepository;
            _friendshipRepository = friendshipRepository;
        }
        public IResult isFriends(string user1, string user2)
        {
            var user1model = _userRepository.Get(u => u.UserName == user1);
            var user2model = _userRepository.Get(u => u.UserName == user2);
            if (user1model == null || user2model == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var friendship = _friendshipRepository.GetListByFilterOrAll(fr => 
                fr.FriendshipConditionId == FriendshipAndFriendshipCondition.Friend 
                &&
                ((fr.SenderUserId == user1model.UserId || fr.RecipientUserId == user1model.UserId)
                    &&
                (fr.SenderUserId == user2model.UserId || fr.RecipientUserId == user2model.UserId))
            );
            if (friendship == null || friendship.Count == 0)
            {
                return new ErrorResult(Messages.UsersAreNotFriends);
            }
            return new SuccessResult(Messages.UsersAreFriends);
        }
    }
}