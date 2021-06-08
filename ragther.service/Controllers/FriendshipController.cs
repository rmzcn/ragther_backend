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
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;


namespace ragther.service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IFriendshipService _friendshipService;
        public FriendshipController(ILogger<UserController> logger, IFriendshipService friendshipService)
        {
            _logger = logger;
            _friendshipService = friendshipService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("send-request")]
        public ActionResult SendFriendship(string senderUserName, string recipientUserName)
        {
            var result = _friendshipService.CreateFriendshipRequest(senderUserName,recipientUserName);
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
        [Route("reject")]
        public ActionResult RejectFriendship(string rejecterUserName, string senderUserName)
        {
            var result = _friendshipService.RejectFriendshipRequest(rejecterUserName,senderUserName);
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
        [Route("revoke")]
        public ActionResult RevokeFriendship(string revokerUserName, string recipientUserName)
        {
            var result = _friendshipService.RevokeFriendshipRequest(revokerUserName,recipientUserName);
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
        [Route("accept")]
        public ActionResult AcceptFreindship(string accepterUserName, string senderUserName)
        {
            var result = _friendshipService.CreateFriendship(senderUserName,accepterUserName);
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
        [Route("get-status")]
        public ActionResult GetFriendship(string requesterUserName, string targetUserName)
        {
            var result = _friendshipService.GetFriendship(requesterUserName,targetUserName);
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
        [Route("get-friends")]
        public ActionResult GetFriends(string requesterUserName)
        {
            var result = _friendshipService.GetFriendsForChatService(requesterUserName);
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