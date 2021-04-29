using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController:ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private ITodoService _todoService;
        private IMapper _mapper;
        public TodoController(ILogger<TodoController> logger, ITodoService todoService, IMapper mapper){
            _logger = logger;
            _todoService = todoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateTodo(VMNewTodoPost model){
            try
            {
                var result = _todoService.Create(model);
                if (result.Success)
                {
                    return CreatedAtAction("Created",result.Message);
                }
                else
                {
                    throw new Exception(result.Message);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{update}")]
        public ActionResult UpdateTodo(string requesterUserName, VMTodoUpdatePost model){
            try
            {
                var result = _todoService.Update(model,requesterUserName);
                if (result.Success)
                {
                    return Ok(result.Message);
                }
                else
                {
                    throw new Exception(result.Message);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{delete}")]
        public ActionResult DeleteTodo(string requesterUserName, int todoID){
            try
            {
                var result = _todoService.Delete(todoID,requesterUserName);
                if (result.Success)
                {
                    return Ok();
                }
                else
                {
                    throw new Exception(result.Message);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{userName}")]
        public ActionResult GetUserTodos(string userName, string requesterUserName){
            // TODO - requesterUserName parametresi GÜVENLİ DEĞİL, silinecek. Sadece geliştirme amacıyla kullanılacaktır.             
            var result = _todoService.GetTodosByUserName(userName, requesterUserName);
            if (result.Success)
            {
                return Ok(result);
            }
            else if (result.Message == Messages.UserNotFound)
            {
                return NotFound(Messages.UserNotFound);
            }
            else if (result.Message == Messages.UsersAreNotFriends)
            {
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("home")]
        public ActionResult GetMainPageTodos(string latitude, string longitude, string requesterUserName, bool near){
            // string requesterUserName Parametresi geliştirme amacıyla tanımlanmıştır
            // bundan sonra jwt yapısı kullanılacaktır
            
            var result = _todoService.GetTodosByLocation(latitude,longitude,requesterUserName,near);
            return Ok(result.Data);
            // if data is null: returns 204 NO CONTENT
        }
    }
}