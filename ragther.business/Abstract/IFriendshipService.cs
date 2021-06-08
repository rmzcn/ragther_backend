using System;
using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface IFriendshipService
    {
        IResult isFriends(string user1, string user2);
        IResult CreateFriendship(string senderUserName, string recipientUserName);
        IResult CreateFriendshipRequest(string senderUserName, string recipientUserName);
        IResult RejectFriendshipRequest(string rejecterUserName, string senderUserName);
        IResult RevokeFriendshipRequest(string revokerUserName, string recipientUserName);
        IDataResult<String> GetFriendshipCondition(string requesterUserName, string targetUserName);
        IResult GetFriendship(string requesterUserName, string targetUserName);
        IDataResult<List<VMInnerUserInfo>> GetFriendsForChatService(string requesterUserName);
    }
}