using ragther.Core.Utilities.Results;
using ragther.entity;

namespace ragther.business.Abstract
{
    public interface IRemindService
    {
        IResult Remind(int todoId, string requesterUserName);
    }
}