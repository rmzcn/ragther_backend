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
    public class MailUpdateController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IMailUpdateService _mailUpdateService;
        public MailUpdateController(ILogger<UserController> logger, IMailUpdateService mailUpdateService)
        {
            _logger = logger;
            _mailUpdateService = mailUpdateService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("send-request")]
        public ActionResult CreateMailUpdateRequest(string requesterUserName, string newMailAdress)
        {
            var result = _mailUpdateService.CreateUpdateRequest(requesterUserName,newMailAdress);
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
        [Route("update")]
        public ActionResult UpdateEmail(string token)
        {
            var result = _mailUpdateService.UpdateEmail(token);
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