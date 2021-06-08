using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ragther.business.Abstract;
using ragther.business.Constants;
using ragther.business.Helpers;
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;
using ragther.service.Hubs;
using ragther.service.TimerFeatures;

namespace ragther.service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoticeController:ControllerBase
    {
        private IHubContext<NoticeHub> _hub;
        private readonly ILogger<UserController> _logger;
        private INoticeService _noticeService;
        public NoticeController(ILogger<UserController> logger, INoticeService noticeService, IHubContext<NoticeHub> hub)
        {
            _hub = hub;
            _logger = logger;
            _noticeService = noticeService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("delete-all")]
        public ActionResult DeleteAllNotices(string requesterUserName)
        {
            var result = _noticeService.DeleteAllNotices(requesterUserName);
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
        [Route("get-notices")]
        public ActionResult GetNotices(string requesterUserName)
        {
            var result = _noticeService.GetNotices(requesterUserName);
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
        [Route("read-notices")]
        public ActionResult ReadNotices(string requesterUserName)
        {
            var result = _noticeService.ReadNotices(requesterUserName);
            if (result.Success)
            {
                return Ok(JSONHelper.ConvertMessageToJSONFormat("message",result.Message));
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }


        [HttpGet]
        [Route("unread-notices-count")]
        public ActionResult GetUnreadNoticesCount(string requesterUserName)
        {
            var result = _noticeService.GetUnreadNoticesCount(requesterUserName);
            // var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transfernoticedata", _noticeService.GetUnreadNoticesCount(user).Message));
            if (result.Success)
            {
                return Ok(JSONHelper.ConvertMessageToJSONFormat("unreadNoticeCount",result.Message));
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }
    }
}