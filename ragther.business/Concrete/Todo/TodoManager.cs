using System;
using System.Collections.Generic;
using AutoMapper;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;
using ragther.entity.ViewModels;

namespace ragther.business.Concrete.Todo
{
    public class TodoManager : ITodoService
    {
        ITodoRepository _todoRepository;
        IUserRepository _userRepository;
        IMapper _mapper;
        IFriendshipService _friendshipService;
        IProfileDetailService _profileDetailService;
        public TodoManager(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper, IFriendshipService friendshipService, IProfileDetailService profileDetailService){
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _friendshipService = friendshipService;
            _profileDetailService = profileDetailService;
            _mapper = mapper;
        }

        public IResult Create(VMNewTodoPost newTodo)
        {
            entity.Todo todo = _mapper.Map<entity.Todo>(newTodo);
            todo.CreatedAt = DateTime.Now;
            todo.TodoConditionId = TodoAndTodoCondition.Working;
            _todoRepository.Add(todo);
            return new SuccessResult(Messages.TodoAdded+todo.TodoId);
        }

        public IResult Delete(int todoID, string requesterUserName)
        {
            var dTodo = _todoRepository.Get( t => t.TodoId == todoID && t.CreatorUser.UserName == requesterUserName);
            if (dTodo == null)
            {
                return new ErrorResult(Messages.TodoNotFound+todoID);
            }
            _todoRepository.Delete(dTodo);
            return new SuccessResult(Messages.TodoDeleted+todoID);
            
        }

        public IResult Update(VMTodoUpdatePost updatedTodo, string requesterUserName)
        {
            entity.Todo tempTodo = _todoRepository.Get(t => t.TodoId == updatedTodo.TodoId && t.CreatorUser.UserName == requesterUserName);
            if (tempTodo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            tempTodo = _mapper.Map<entity.Todo>(updatedTodo);
            _todoRepository.Update(tempTodo);
            return new SuccessResult(Messages.TodoUpdated);
        }

        public IDataResult<VMTodoGet> GetTodoById(int todoID, string requesterUserName)
        {
            var todo = _todoRepository.Get(td => td.TodoId == todoID);
            if (todo == null)
            {
                return new ErrorDataResult<VMTodoGet>(Messages.TodoNotFound);
            }
            if (_friendshipService.isFriends(todo.CreatorUser.UserName, requesterUserName).Success)
            {
                VMTodoGet result = _mapper.Map<VMTodoGet>(todo);
                return new SuccessDataResult<VMTodoGet>(result);
            }
            else
            {
                return new ErrorDataResult<VMTodoGet>(Messages.UsersAreNotFriends);
            }
        }

        public IDataResult<List<VMTodoGet>> GetTodosByLocation(string latitude, string longitude, string requesterUserName, bool near = true)
        {
            // TODO - Arkadaş olmayan kullanıcıların todolarını getirme
            // TODO - Data layer da raw sql ile BELİRLİ sayıda todo getir. Geçerli yöntem çok verimsiz.

            List<VMTodoGet> todoList = _todoRepository.GetMainPageTodos(requesterUserName,0);
            
            if (todoList == null || todoList.Count == 0)
            {
                return new ErrorDataResult<List<VMTodoGet>>(Messages.TodoCountZero);
            }

            if (String.IsNullOrEmpty(latitude) || String.IsNullOrEmpty(longitude))
            {
                near = false;
            }
            
            if (near)
            {
                var sortedByLocation = this.SortByLocation(todoList, latitude,longitude);
                if (sortedByLocation != null)
                {
                    todoList = sortedByLocation;
                }
                else
                {
                    return new ErrorDataResult<List<VMTodoGet>>(todoList,Messages.InvalidLocation);
                }
            }
            return new SuccessDataResult<List<VMTodoGet>>(todoList);
        }

        private List<VMTodoGet> SortByLocation(List<VMTodoGet> todoList, string latitude, string longitude)
        {
            // formul = sqrt(pow((lat1-lat2), 2) + pow((long1-long2), 2))
            // used bubble sort because of its simple using
            // but if todoList parameter is big, sorting algorithm can be change

            try
            {
                List<VMTodoGet> result = todoList;
                double firstDistance;
                double secondDistance;
                VMTodoGet temp;
                for (int i = 0; i < result.Count -1; i++)
                {
                    for (int j = 1; j < result.Count -1; j++)
                    {
                        firstDistance = this.CalculateDistance(latitude,longitude,result[j-1].LocationLatitude,result[j-1].LocationLongitude);
                        secondDistance = this.CalculateDistance(latitude,longitude,result[j].LocationLatitude,result[j].LocationLongitude);
                        if (firstDistance > secondDistance)
                        {
                            temp = result[j - 1];
                            result[j - 1] = result[j];
                            result[j] = temp;
                        }
                    }
                }
                return result;
            }
            catch (System.Exception)
            {
                return null;
            }
            
        }
        private double CalculateDistance(string latitude, string longitude, string todoLatitude, string todoLongitude){
            double distance = Math.Sqrt(
                        (Math.Pow(double.Parse(latitude, System.Globalization.CultureInfo.InvariantCulture),2)
                            -
                        Math.Pow(double.Parse(todoLatitude, System.Globalization.CultureInfo.InvariantCulture),2))
                        +
                        (Math.Pow(double.Parse(longitude, System.Globalization.CultureInfo.InvariantCulture),2)
                            -
                        Math.Pow(double.Parse(todoLongitude, System.Globalization.CultureInfo.InvariantCulture),2))
                    );
            return distance;
        }

        public IDataResult<List<VMTodoGet>> GetTodosByUserName(string userName, string requesterUserName)
        {
            // TODO - Data layer da sql ile BELİRLİ sayıda todo getir. Bu hali ile bellek boşuna dolduruyor. Bunu düzlet.

            // if friend or users profile is not hidden show todos
            if (_friendshipService.isFriends(userName, requesterUserName).Success || !_profileDetailService.IsHiddenProfile(userName).Success)
            {
                var todos = _todoRepository.GetListByFilterOrAll(t => t.CreatorUser.UserName == userName);
                if (todos == null)
                {
                    return new ErrorDataResult<List<VMTodoGet>>(Messages.UserNotFound);
                }
                List<VMTodoGet> result = _mapper.Map<List<VMTodoGet>>(todos);
                return new SuccessDataResult<List<VMTodoGet>>(result);

            }
            return new ErrorDataResult<List<VMTodoGet>>(Messages.UsersAreNotFriends);
        }

    }
}