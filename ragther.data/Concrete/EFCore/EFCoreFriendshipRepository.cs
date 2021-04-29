using ragther.data.Abstract;
using ragther.entity;
using ragther.core.DataAccess.EFCore;

namespace ragther.data.Concrete.EFCore
{
    public class EFCoreFriendshipRepository : EFCoreGenericRepository<Friendship,RagtherDbContext>, IFriendshipRepository
    {
        
    }
}