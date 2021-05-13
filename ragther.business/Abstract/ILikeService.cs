using ragther.Core.Utilities.Results;
using ragther.entity;

namespace ragther.business.Abstract
{
    public interface ILikeService
    {
        IResult Like(int todoId, string requesterUserName);
        IResult UnLike(int todoId, string requesterUserName);
    }
}