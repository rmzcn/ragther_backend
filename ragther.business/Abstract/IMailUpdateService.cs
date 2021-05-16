using ragther.Core.Utilities.Results;
using ragther.entity;

namespace ragther.business.Abstract
{
    public interface IMailUpdateService
    {
        IResult CreateUpdateRequest(string requesterUserName, string newMailAdress);
        IResult UpdateEmail(string token);
    }
}