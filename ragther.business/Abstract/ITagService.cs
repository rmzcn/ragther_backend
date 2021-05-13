using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface ITagService
    {
        IDataResult<List<VMTagGet>> GetTagsByFilter(string filter, string requesterUserName); 
        IResult CreateTag(VMNewTagPost model, string requesterUserName);
    }
}