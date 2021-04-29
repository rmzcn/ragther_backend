using ragther.entity;
using ragther.data.Abstract;
using ragther.core.DataAccess.EFCore;

namespace ragther.data.Concrete.EFCore
{
    public class EFCoreLikeRepository : EFCoreGenericRepository<Like,RagtherDbContext>, ILikeRepository
    {
        
    }
}