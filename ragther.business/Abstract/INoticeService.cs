using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface INoticeService
    {
        IResult CreateNotice(int userIdFrom, int userIdTo, int noticeTypeId, string relevantIdOrUrl = null);
        IDataResult<List<VMNoticeGet>> GetNotices(string requesterUserName);
        IResult DeleteAllNotices(string requesterUserName);
        IResult ReadNotices(string requesterUserName);
    }
}