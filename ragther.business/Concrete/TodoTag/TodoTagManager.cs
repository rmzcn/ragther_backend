using System.Collections.Generic;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using ragther.data.Abstract;

namespace ragther.business.Concrete.TodoTag
{
    public class TodoTagManager : ITodoTagService
    {
        IUserRepository _userRepository;
        ITodoTagRepository _todoTagRepository;
        ITagRepository _tagRepository;

        ITodoRepository _todoRepository;

        public TodoTagManager(IUserRepository userRepository, ITodoTagRepository todoTagRepository, ITagRepository tagRepository, ITodoRepository todoRepository){
            _userRepository = userRepository;
            _todoTagRepository = todoTagRepository;
            _tagRepository = tagRepository;
            _todoRepository = todoRepository;
        }
        public IResult AddTag(int todoId, int tagId)
        {
            var tag = _tagRepository.Get( t => t.TagId == tagId);
            if (tag == null)
            {
                return new ErrorResult(Messages.TagNotFound);
            }
            var todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            var newTodoTag = new entity.TodoTag(){
                TodoId = todoId,
                TagId = tag.TagId
            };
            _todoTagRepository.Add(newTodoTag);
            return new SuccessResult();
        }

        public IResult DeleteTag(int todoId, int tagId)
        {
            var tag = _tagRepository.Get( t => t.TagId == tagId);
            if (tag == null)
            {
                return new ErrorResult(Messages.TagNotFound);
            }
            var todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }
            var newTodoTag = new entity.TodoTag(){
                TodoId = todoId,
                TagId = tag.TagId
            };
            _todoTagRepository.Delete(newTodoTag);
            return new SuccessResult();
        }


        public IResult UpdateTodoTags(int todoId, List<int> tagIdList)
        {
            var todo = _todoRepository.Get( t => t.TodoId == todoId);
            if (todo == null)
            {
                return new ErrorResult(Messages.TodoNotFound);
            }

            //TODO - CONTAINSI DÜZELT
            // List<entity.Tag> currentTagsOfTodo = _tagRepository.GetListByFilterOrAll( u => 
            //     u.TodoTags.Contains(_todoTagRepository.Get( w => w.TodoId == todoId))
            // );

            List<entity.Tag> currentTagsOfTodo = new List<entity.Tag>();
            List<entity.TodoTag> todoTags = _todoTagRepository.GetListByFilterOrAll( w => w.TodoId == todoId);
            if (todoTags != null)
            {
                foreach (var todoTag in todoTags)
                {
                    currentTagsOfTodo.Add(_tagRepository.Get(t => t.TagId == todoTag.TagId));
                }
            }



            // adding
            bool isTagInUse;
            foreach (var todoTagId in tagIdList)
            {
                isTagInUse = false;
                var tag = _tagRepository.Get( u => u.TagId == todoTagId);
                if (tag != null)
                {
                    foreach (var currentTag in currentTagsOfTodo)
                    {
                        if (currentTag.TagId == todoTagId)
                        {
                            isTagInUse = true;
                            break;
                        }
                    }
                    
                    if (!isTagInUse)
                    {
                        this.AddTag(todoId, todoTagId);
                    }
                }
            }

            // deleting
            foreach (var currentTag in currentTagsOfTodo)
            {
                isTagInUse = false;
                foreach (var todoTagId in tagIdList)
                {
                    if (currentTag.TagId == todoTagId)
                    {
                        isTagInUse = true;
                        break;
                    }
                }
                
                if (!isTagInUse)
                {
                    this.DeleteTag(todoId, currentTag.TagId);
                }
                
            }
            return new SuccessResult(Messages.WorkersUpdated);
        }
        
    }
}