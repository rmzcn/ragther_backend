using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;


namespace ragther.business.Abstract
{
    public interface IChatService
    {
        IResult CreateChat(string requesterUserName, string secondUserName);
        IDataResult<List<VMChatModelGet>> GetChats(string requesterUserName);
        IDataResult<VMChatHeadModelGet> GetChatHead(string requesterUserName, int chatId);
    }
}