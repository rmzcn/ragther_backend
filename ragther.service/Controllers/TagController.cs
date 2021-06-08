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
    public class TagController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ITagService _tagService;
        public TagController(ILogger<UserController> logger, ITagService tagService){
            _logger = logger;
            _tagService = tagService;
        }

        [AllowAnonymous]
        [HttpPost]
        // [Route("{requesterUserName}")]
        public ActionResult CreateTag(VMNewTagPost model, string requesterUserName)
        {
            var result = _tagService.CreateTag(model,requesterUserName);
            if(result.Success)
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
        [Route("getbyfilter")]
        public ActionResult GetTagsByFilter(string filter, string requesterUserName)
        {
            var result = _tagService.GetTagsByFilter(filter,requesterUserName);
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