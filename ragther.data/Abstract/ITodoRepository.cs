using System.Collections.Generic;
using ragther.entity;
using ragther.core.DataAccess;
using ragther.entity.ViewModels;

namespace ragther.data.Abstract
{
    public interface ITodoRepository: IRepository<Todo>
    {
        List<VMTodoGet> GetMainPageTodos(string requesterUserName, int pageNumber, int batchSize = 15);
    }
}