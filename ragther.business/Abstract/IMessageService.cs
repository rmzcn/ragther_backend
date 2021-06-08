using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface IMessageService
    {
        IResult SendMessage(string senderUserName, int chatId, string content);
        IDataResult<List<VMChatMessageGet>> GetMessages(int chatId, string requesterUserName);
        IDataResult<List<VMChatMessageGet>> GetUnreadMessages(int chatId, string requesterUserName);
    }
}