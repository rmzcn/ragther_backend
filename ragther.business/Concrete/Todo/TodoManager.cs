using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;
using ragther.data.MessagesForRelations;
using ragther.entity.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

namespace ragther.business.Concrete.Todo
{
    public class TodoManager : ITodoService
    {
        ITodoRepository _todoRepository;
        IUserRepository _userRepository;
        IMapper _mapper;
        IFriendshipService _friendshipService;
        IProfileDetailService _profileDetailService;
        ITodoTagRepository _todoTagRepository;
        ITodoTagService _todoTagService;
        IWorkWithService _workWithService;
        ITagRepository _tagRepository;
        IWorkWithRepository _workWithRepository;
        public TodoManager(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper, IFriendshipService friendshipService, IProfileDetailService profileDetailService, ITodoTagRepository todoTagRepository, ITagRepository tagRepository,IWorkWithRepository workWithRepository, ITodoTagService todoTagService, IWorkWithService workWithService){
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _todoTagRepository = todoTagRepository;
            _tagRepository = tagRepository;
            _friendshipService = friendshipService;
            _workWithService = workWithService;
            _profileDetailService = profileDetailService;
            _todoTagService = todoTagService;
            _mapper = mapper;
            _workWithRepository = workWithRepository;
        }

        //WORKING
        public IResult Create(VMNewTodoPost newTodo)
        {
            entity.Todo todo = _mapper.Map<entity.Todo>(newTodo);
            todo.CreatedAt = DateTime.Now;
            todo.TodoConditionId = TodoAndTodoCondition.working;
            _todoRepository.Add(todo);
            
            foreach (var todoTagName in newTodo.tags)
            {
                _todoTagService.AddTag(todo.TodoId, _tagRepository.Get( t=> t.Name == todoTagName).TagId);
            }
            return new SuccessResult(todo.TodoId.ToString());
        }

        //WORKING
        public IResult Delete(int todoID, string requesterUserName)
        {
            var dTodo = _todoRepository.Get( t => t.TodoId == todoID && t.CreatorUser.UserName == requesterUserName);
            if (dTodo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            _todoRepository.Delete(dTodo);
            return new SuccessResult(Messages.TodoDeleted);
            
        }

        //WORKING
        public IResult Update(VMTodoUpdatePost updatedTodo, string requesterUserName)
        {
            entity.Todo tempTodo = _todoRepository.Get(t => t.TodoId == updatedTodo.TodoId && t.CreatorUser.UserName == requesterUserName);
            if (tempTodo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            
            // occured mapping error
            // var todo = _mapper.Map<entity.Todo>(updatedTodo);
            // used manuel mapping in below
            entity.Todo todo = new entity.Todo(){
                TodoId = updatedTodo.TodoId,
                imageUrl = updatedTodo.imageUrl,
                Description = updatedTodo.Description,
                UntilWhen = updatedTodo.UntilWhen,
                LocationLatitude = updatedTodo.LocationLatitude,
                LocationLongitude = updatedTodo.LocationLongitude
            };

            List<int> todoTagIdList = new List<int>();
            // WARNING START
            entity.Tag tempTag;
            foreach (var todoTagName in updatedTodo.tags)
            {
                tempTag = _tagRepository.Get( t => t.Name == todoTagName );
                if (tempTag != null)
                {
                    todoTagIdList.Add(tempTag.TagId);
                }
            }

            Console.WriteLine("*****************************");
            Console.WriteLine(todoTagIdList.Count);
            Console.WriteLine("*****************************");
            // WARNING END
            _todoTagService.UpdateTodoTags(updatedTodo.TodoId,todoTagIdList);

            _workWithService.UpdateWorkers(updatedTodo.TodoId, updatedTodo.workWiths);

            todo.CreatorUserId = tempTodo.CreatorUserId;
            todo.TodoConditionId = tempTodo.TodoConditionId;
            todo.UpdatedAt = DateTime.Now;

            

            _todoRepository.Update(todo);
            return new SuccessResult(Messages.TodoUpdated);
        }

        //WORKING
        public IDataResult<VMTodoGet> GetTodoById(int todoID, string requesterUserName)
        {
            var todo = _todoRepository.Get(td => td.TodoId == todoID);
            
            if (todo == null)
            {
                return new ErrorDataResult<VMTodoGet>(Messages.TodoNotFound);
            }
            todo.CreatorUser = _userRepository.Get(u => u.UserId == todo.CreatorUserId);
            if (_friendshipService.isFriends(todo.CreatorUser.UserName, requesterUserName).Success || !_profileDetailService.IsHiddenProfile(todo.CreatorUser.UserName).Success || todo.CreatorUser.UserName == requesterUserName)
            {
                VMTodoGet result = _mapper.Map<VMTodoGet>(todo);
                result.userInfo = _mapper.Map<VMInnerUserInfo>(todo.CreatorUser);
                result.TodoCondition = TodoAndTodoCondition.getTodoConditionByID(todo.TodoConditionId);

                var todoTags = _todoTagRepository.GetListByFilterOrAll( t => t.TodoId == result.TodoId);
                var workWiths = _workWithRepository.GetListByFilterOrAll( w => w.TodoId == result.TodoId);

                result.tags = new List<string>();
                result.workWiths = new List<string>();

                if (todoTags != null)
                {
                    foreach (var todoTag in todoTags)
                    {                     
                        result.tags.Add(_tagRepository.Get(t => t.TagId == todoTag.TagId).Name);
                    }
                }
                if (workWiths != null)
                {
                    foreach (var workWith in workWiths)
                    {
                        result.workWiths.Add(_userRepository.Get( u => u.UserId == workWith.UserId).UserName);
                    }
                }
                
                

                return new SuccessDataResult<VMTodoGet>(result);
            }
            else
            {
                return new ErrorDataResult<VMTodoGet>(Messages.UsersAreNotFriends);
            }
        }

        //WORKING
        public IDataResult<List<VMTodoGet>> GetTodosByLocation(string latitude, string longitude, string requesterUserName, bool near = true)
        {
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
            
            foreach (var todo in todoList)
            {
                var todoTags = _todoTagRepository.GetListByFilterOrAll( t => t.TodoId == todo.TodoId);
                var workWiths = _workWithRepository.GetListByFilterOrAll( w => w.TodoId == todo.TodoId);
                
                todo.tags = new List<string>();
                todo.workWiths = new List<string>();

                if (todoTags != null)
                {
                    foreach (var todoTag in todoTags)
                    {                     
                        todo.tags.Add(_tagRepository.Get(t => t.TagId == todoTag.TagId).Name);
                    }
                }
                if (workWiths != null)
                {
                    foreach (var workWith in workWiths)
                    {
                        todo.workWiths.Add(_userRepository.Get( u => u.UserId == workWith.UserId).UserName);
                    }
                }
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

        //WORKING
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
        
        //WORKING
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

        //WORKING
        public IDataResult<List<VMTodoGet>> GetTodosByUserName(string userName, string requesterUserName)
        {
            // TODO - Data layer da sql ile BELİRLİ sayıda todo getir. Bu hali ile bellek boşuna dolduruyor. Bunu düzlet.

            // if friend or users profile is not hidden show todos
            if (_friendshipService.isFriends(userName, requesterUserName).Success || !_profileDetailService.IsHiddenProfile(userName).Success || userName == requesterUserName)
            {
                var todos = _todoRepository.GetListByFilterOrAll(t => t.CreatorUser.UserName == userName);
                if (todos == null)
                {
                    return new ErrorDataResult<List<VMTodoGet>>(Messages.UserNotFound);
                }
                
                List<VMTodoGet> result = _mapper.Map<List<VMTodoGet>>(todos);
                foreach (var todo in todos)
                {
                    todo.CreatorUser = _userRepository.Get(u => u.UserId == todo.CreatorUserId);
                }
                foreach (var res in result)
                {
                    res.userInfo = _mapper.Map<VMInnerUserInfo>(todos.Where(t => t.TodoId == res.TodoId).FirstOrDefault().CreatorUser);
                    var todoTags = _todoTagRepository.GetListByFilterOrAll( t => t.TodoId == res.TodoId);
                    var workWiths = _workWithRepository.GetListByFilterOrAll( w => w.TodoId == res.TodoId);

                    res.tags = new List<string>();
                    res.workWiths = new List<string>();

                    if (todoTags != null)
                    {
                        foreach (var todoTag in todoTags)
                        {                     
                            res.tags.Add(_tagRepository.Get(t => t.TagId == todoTag.TagId).Name);
                        }
                    }
                    if (workWiths != null){
                        foreach (var workWith in workWiths)
                        {
                            res.workWiths.Add(_userRepository.Get( u => u.UserId == workWith.UserId).UserName);
                        }
                    }
                    
                    res.TodoCondition = TodoAndTodoCondition.getTodoConditionByID(todos.Where(t => t.TodoId == res.TodoId).FirstOrDefault().TodoConditionId);
                }
                return new SuccessDataResult<List<VMTodoGet>>(result);

            }
            return new ErrorDataResult<List<VMTodoGet>>(Messages.UsersAreNotFriends);
        }

        //WORKING
        public IDataResult<List<VMTodoGet>> GetTodoByTagName(string tagName, string requesterUserName)
        {
            //TODO - Arkadaş olmayan kişilerin todolarını getirme.

            // var todos = _todoRepository.GetListByFilterOrAll( t =>
            //     t.TodoTags.Contains(_todoTagRepository.Get(tt => tt.Tag.Name == tagName))                
            // );
            
            var tempTodoTags = _todoTagRepository.GetListByFilterOrAll( tt => tt.Tag.Name == tagName);
            var todos = new List<entity.Todo>();
            foreach (var tempTodoTag in tempTodoTags)
            {
                todos.Add(_todoRepository.Get(t => tempTodoTag.TodoId == t.TodoId));
            }

            var result = _mapper.Map<List<VMTodoGet>>(todos);
            foreach (var todo in result)
            {
                // -todo'nun user info alanına user repodan gelen veriyi map ediyor.
                // -todo.userInfo = _mapper.Map<VMInnerUserInfo>(_userRepository.Get( u => u.Todos.Contains(
                //     _todoRepository.Get( ut => ut.TodoId == todo.TodoId)
                // )));

                var user = _userRepository.Get( u => u.Todos.Where( ut => ut.TodoId == todo.TodoId).FirstOrDefault().TodoId == todo.TodoId);
                todo.userInfo = _mapper.Map<VMInnerUserInfo>(user);

                //ilk başta boş olarak tanımlayıp ardından aşağıda dolduruyoruz db den.
                todo.tags = new List<string>();
                todo.workWiths = new List<string>();

                foreach (entity.Todo todoForGettingCondition in todos)
                {
                    if (todoForGettingCondition.TodoId == todo.TodoId)
                    {
                        todo.TodoCondition = TodoAndTodoCondition.getTodoConditionByID(todoForGettingCondition.TodoConditionId);
                    }
                     
                }
                
                // -todonun etiketleri ve birlikte çalışılanları
                var todoTags = _todoTagRepository.GetListByFilterOrAll( t => t.TodoId == todo.TodoId);
                var workWiths = _workWithRepository.GetListByFilterOrAll( w => w.TodoId == todo.TodoId);
                if (todoTags != null)
                {
                    foreach (var todoTag in todoTags)
                    {                          
                        todo.tags.Add(_tagRepository.Get(t => t.TagId == todoTag.TagId).Name);
                    }
                }
                if (workWiths != null)
                {
                    foreach (var workWith in workWiths)
                    {
                        todo.workWiths.Add(_userRepository.Get( u => u.UserId == workWith.UserId).UserName);
                    }
                }
            }
            return new SuccessDataResult<List<VMTodoGet>>(result);
        }

        public IResult UploadTodoImage(int todoId, string requesterUserName, IFormFile file)
        {
            var user = _userRepository.Get(u => u.UserName == requesterUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var todo = _todoRepository.Get(t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }

            try
            {
                var folderName = Path.Combine("Resources","Images","PostImages");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                
                if (file.Length > 0)
                {
                    
                    // var fileName = todo.TodoId.ToString() + "_" + user.UserId.ToString() + user.UserName + "_" + DateTime.Now.ToString()+Path.GetExtension(file.FileName);

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    var fullPath = Path.Combine(pathToSave, fileName);          
                    
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }

                    var dbPath = Path.Combine(folderName, fileName);

                    todo.imageUrl = dbPath;
                    _todoRepository.Update(todo);
                    return new SuccessResult(Messages.FileUploaded);
                }
                return new ErrorResult(Messages.FileLengthZero);
            }
            catch (System.Exception ex)
            {
               return new ErrorResult(ex.Message);
            }
        }
    }
}