using System.Collections.Generic;
using ragther.Core.Utilities.Results;
using ragther.entity;

namespace ragther.business.Abstract
{
    public interface IWorkWithService
    {
        IDataResult<List<entity.User>> GetWorkerUsersByTodoId(int todoId);
        IResult AddWorker(int todoId, string workerUserName);
        IResult DeleteWorker(int todoId, string workerUserName);
        IResult UpdateWorkers(int todoId, List<string> workerUserNames);
    }
}