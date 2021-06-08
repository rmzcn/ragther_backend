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
    public class RemindController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IRemindService _remindService;
        public RemindController(ILogger<UserController> logger, IRemindService remindService){
            _logger = logger;
            _remindService = remindService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Remind(int todoId, string requesterUserName)
        {
            var result = _remindService.Remind(todoId, requesterUserName);
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