using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface ICommentService
    {
        IResult Create(VMNewCommentPost newCommentPost);
        IResult Delete(int commentID, string requesterUserName);
        IResult AcceptOffer(int commentID, string requesterUserName);
        IResult RejectOffer(int commentID, string requesterUserName);
        IDataResult<List<VMCommentGet>> GetTodoComments(int todoID);
    }
}