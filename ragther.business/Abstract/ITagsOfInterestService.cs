using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface ITagsOfInterestService
    {
        IResult SetInterestedTags(string requesterUserName, List<int> tagIdList);
        IDataResult<List<VMTagGet>> GetInterestedTags(string requesterUserName);
    }
}