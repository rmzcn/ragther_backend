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
    public class MessageController:ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private IMessageService _messageService;
        public MessageController(ILogger<TodoController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("send-message")]
        public ActionResult SendMessage(string senderUserName, int chatId, string content)
        {
            var result = _messageService.SendMessage(senderUserName, chatId, content);
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
        [Route("get-interval")]
        public ActionResult GetUnreadMessages(int chatId, string requesterUserName)
        {
            var result = _messageService.GetUnreadMessages(chatId, requesterUserName);
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
        [Route("get-messages")]
        public ActionResult GetMessages(int chatId, string requesterUserName)
        {
            var result = _messageService.GetMessages(chatId, requesterUserName);
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