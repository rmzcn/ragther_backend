using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;

namespace ragther.business.Abstract
{
    public interface ITodoTagService
    {
        IResult AddTag(int todoId, int tagId);
        IResult DeleteTag(int todoId, int tagId);
        IResult UpdateTodoTags(int todoId, List<int> tagIdList);
    }
}