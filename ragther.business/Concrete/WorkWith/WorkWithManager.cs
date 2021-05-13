using System.Collections.Generic;
using AutoMapper;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;

namespace ragther.business.Concrete.WorkWith
{
    public class WorkWithManager : IWorkWithService
    {
        ITodoRepository _todoRepository;
        IUserRepository _userRepository;
        IMapper _mapper;
        IWorkWithRepository _workWithRepository;
        public WorkWithManager(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper, IWorkWithRepository workWithRepository){
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _workWithRepository = workWithRepository;
            _mapper = mapper;
        }

        public IResult AddWorker(int todoId, string workerUserName)
        {
            var user = _userRepository.Get(u => u.UserName == workerUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            var newWorkWith = new entity.WorkWith(){
                TodoId = todoId,
                UserId = user.UserId
            };
            _workWithRepository.Add(newWorkWith);
            return new SuccessResult();
        }

        public IResult DeleteWorker(int todoId, string workerUserName)
        {
            var user = _userRepository.Get(u => u.UserName == workerUserName);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            var newWorkWith = new entity.WorkWith(){
                TodoId = todoId,
                UserId = user.UserId
            };
            _workWithRepository.Delete(newWorkWith);
            return new SuccessResult();
        }

        public IResult UpdateWorkers(int todoId, List<string> workerUserNames)
        {
            var todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }

            //TODO - CONTAINSI DÜZELT
            
            // var currentWorkersOfTodo = _userRepository.GetListByFilterOrAll( u => 
            //     u.WorkWiths.Contains(_workWithRepository.Get( w => w.TodoId == todoId))
            // );

            List<entity.User> currentWorkersOfTodo = new List<entity.User>();
            List<entity.WorkWith> workWiths = _workWithRepository.GetListByFilterOrAll( w => w.TodoId == todoId);
            if (workWiths != null)
            {
                foreach (var workWith in workWiths)
                {
                    currentWorkersOfTodo.Add(_userRepository.Get(t => t.UserId == workWith.UserId));
                }
            }

            // adding
            bool isUserWorker;
            foreach (var userName in workerUserNames)
            {
                isUserWorker = false;
                var user = _userRepository.Get( u => u.UserName == userName);
                if (user != null)
                {
                    foreach (var workerUser in currentWorkersOfTodo)
                    {
                        if (workerUser.UserName == userName)
                        {
                            isUserWorker = true;
                            break;
                        }
                    }
                    
                    if (!isUserWorker)
                    {
                        this.AddWorker(todoId, userName);
                    }
                }
            }

            // deleting
            foreach (var workerUser in currentWorkersOfTodo)
            {
                isUserWorker = false;
                foreach (var workerUserName in workerUserNames)
                {
                    if (workerUser.UserName == workerUserName)
                    {
                        isUserWorker = true;
                        break;
                    }
                }
                
                if (!isUserWorker)
                {
                    this.DeleteWorker(todoId, workerUser.UserName);
                }
                
            }
            return new SuccessResult(Messages.WorkersUpdated);
        }

        public IDataResult<List<entity.User>> GetWorkerUsersByTodoId(int todoId)
        {
            var todo = _todoRepository.Get(t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorDataResult<List<entity.User>>(Messages.TodoNotFound);
            }
            var result = _userRepository.GetListByFilterOrAll(u => 
                u.WorkWiths.Contains(_workWithRepository.Get( w => w.TodoId == todoId))
            );
            return new SuccessDataResult<List<entity.User>>(result);
        }

        
    }
}