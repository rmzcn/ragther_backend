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
    public class ProfileUpdateController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IProfileUpdateService _profileUpdateService;
        public ProfileUpdateController(ILogger<UserController> logger, IProfileUpdateService profileUpdateService)
        {
            _logger = logger;
            _profileUpdateService = profileUpdateService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("upload-profile-image")]
        public ActionResult UploadProfileImage(string requesterUserName)
        {
            var file = Request.Form.Files[0];
            var result = _profileUpdateService.ProfileImageUpload(requesterUserName,file);
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
        [Route("set-visibility")]
        public ActionResult SetProfileVisibility(string requesterUserName, bool visible)
        {
            var result = _profileUpdateService.SetProfileVisibility(requesterUserName,visible);
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
        [Route("set-hidden-description")]
        public ActionResult UpdateHiddenDescription(string requesterUserName, string newHiddenDescription)
        {
            var result = _profileUpdateService.UpdateHiddenProfileDescription(requesterUserName,newHiddenDescription);
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
        [Route("set-profile-description")]
        public ActionResult UpdateProfileDescription(string requesterUserName, string newDescription)
        {
            var result = _profileUpdateService.UpdateProfileDescription(requesterUserName,newDescription);
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