using ragther.data.Abstract;
using ragther.entity;
using ragther.core.DataAccess.EFCore;


namespace ragther.data.Concrete.EFCore
{
    public class EFCoreFriendshipConditionRepository: EFCoreGenericRepository<FriendshipCondition,RagtherDbContext>, IFriendshipConditionRepository
    {
        
    }
}