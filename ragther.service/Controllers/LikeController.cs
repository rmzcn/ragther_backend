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
    public class LikeController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ILikeService _likeService;
        public LikeController(ILogger<UserController> logger, ILikeService likeService)
        {
            _logger = logger;
            _likeService = likeService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("like")]
        public ActionResult Like(int todoId, string requesterUserName)
        {
            var result = _likeService.Like(todoId, requesterUserName);
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
        [Route("unlike")]
        public ActionResult UnLike(int todoId, string requesterUserName)
        {
            var result = _likeService.UnLike(todoId, requesterUserName);
            if (result.Success)
            {
                return Ok(JSONHelper.ConvertMessageToJSONFormat("message",result.Message));
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }
    }
}