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
using ragther.Core.Utilities.Results;
using ragther.entity;
using ragther.entity.ViewModels;

namespace ragther.service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoticeController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private INoticeService _noticeService;
        public NoticeController(ILogger<UserController> logger, INoticeService noticeService)
        {
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
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
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
                return BadRequest(result.Message);
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
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}