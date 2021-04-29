using ragther.Core.Utilities.Results;
using ragther.entity;

namespace ragther.business.Abstract
{
    public interface IFriendshipService
    {
        IResult isFriends(string user1, string user2);
    }
}