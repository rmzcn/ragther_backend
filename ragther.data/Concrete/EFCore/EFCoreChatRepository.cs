using ragther.core.DataAccess.EFCore;
using ragther.data.Abstract;
using ragther.entity;

namespace ragther.data.Concrete.EFCore
{
    public class EFCoreChatRepository: EFCoreGenericRepository<Chat,RagtherDbContext>, IChatRepository
    {
        
    }
}