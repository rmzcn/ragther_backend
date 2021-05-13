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
    public class CommentController:ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private ICommentService _commentService;
        public CommentController(ILogger<TodoController> logger, ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public ActionResult CreateComment(VMNewCommentPost model, string requesterUserName)
        {
            try
            {
                var result = _commentService.Create(model);
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
        [HttpGet]
        [Route("delete")]
        public ActionResult DeleteComment(int commentID, string requesterUserName)
        {
            var result = _commentService.Delete(commentID,requesterUserName);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return NotFound(result.Message);
            }      
               
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetTodoComments(int todoID)
        {
            var result = _commentService.GetTodoComments(todoID);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }  
        }
    }
}