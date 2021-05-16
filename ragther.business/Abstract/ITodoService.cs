using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.business.Abstract
{
    public interface ITodoService
    {
        IResult Create(VMNewTodoPost newTodo);
        IResult Delete(int todoID, string requesterUserName);
        IResult Update(VMTodoUpdatePost updatedTodo, string requesterUserName);
        IDataResult<List<VMTodoGet>> GetTodosByLocation(string latitude, string longitude, string requesterUserName, bool near);
        IDataResult<List<VMTodoGet>> GetTodosByUserName(string userName, string requesterUserName);
        IDataResult<VMTodoGet> GetTodoById(int todoID, string requesterUserName);
        IDataResult<List<VMTodoGet>> GetTodoByTagName(string tagName, string requesterUserName);
        IResult UploadTodoImage(int todoId, string requesterUserName, IFormFile file);
    }
}