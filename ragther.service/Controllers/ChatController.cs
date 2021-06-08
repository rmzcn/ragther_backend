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
using ragther.business.Helpers;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.service.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController:ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private IChatService _chatService;
        public ChatController(ILogger<TodoController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("create")]
        public ActionResult CreateChat(string requesterUserName, string secondUserName)
        {
            var result = _chatService.CreateChat(requesterUserName, secondUserName);
            if (result.Success)
            {
                return Ok(JSONHelper.ConvertMessageToJSONFormat("message",result.Message));
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-chats")]
        public ActionResult GetChats(string requesterUserName)
        {
            var result = _chatService.GetChats(requesterUserName);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-head")]
        public ActionResult GetChatHead(string requesterUserName, int chatId)
        {
            var result = _chatService.GetChatHead(requesterUserName,chatId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }


    }
}