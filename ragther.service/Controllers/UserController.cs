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
    public class UserController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;
        private IMapper _mapper;
        public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper){
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{userName}/profile")]
        public ActionResult GetUserForProfile(string userName, string requesterUserName){
            // TODO - Şimdilik userid query string ile alınıyor ancak ileride token ile bir service kullanarak kullanıcı adı alınıp GetUserProfile'a parametre olarak gidebilir.
            // TODO - Bu yöntem sadece geliştirme amaçlı bu şekilde kullanılmaktadır ve GÜVENLİ DEĞİLDİR. Nihai sürümde yer almayacaktır.
            var result = _userService.GetUserProfile(userName, requesterUserName);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                if (result.Message == Messages.UserNotFound)
                {
                    return NotFound(JSONHelper.ConvertMessageToJSONFormat("result",result.Message));
                }
                else if (result.Message == Messages.UsersAreNotFriends)
                {
                    return Unauthorized(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
                }
                else
                {
                    return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("password/refresh")]
        public ActionResult ForgetPassword(string newEmail)
        {
            var result = _userService.ForgetPassword(newEmail);
            if (result.Success)
            {
                return Ok(JSONHelper.ConvertMessageToJSONFormat("result",result.Message));
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("password/update")]
        public ActionResult SetPassword(string requesterUserName, string oldPassword, string newPassword)
        {
            var result = _userService.SetPassword(requesterUserName,oldPassword,newPassword);
            if (result.Success)
            {
                return Ok(JSONHelper.ConvertMessageToJSONFormat("result",result.Message));
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("password/get")]
        public ActionResult GetPassword(string requesterUserName)
        {
            var result = _userService.GetPassword(requesterUserName);
            if (result.Success)
            {
                return Ok(JSONHelper.ConvertMessageToJSONFormat("password",result.Message));
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register(VMUserRegisterPost model){
            var result = _userService.Register(model);
            if (result.Success)
            {
                return Ok(JSONHelper.ConvertMessageToJSONFormat("result",result.Message));
            }
            else
            {
                return BadRequest(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(VMUserLoginPost model){
            var result = _userService.Login(model.UserName, model.Password);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return Unauthorized(JSONHelper.ConvertMessageToJSONFormat("error",result.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet("search/{filterString}")]
        public ActionResult GetUsersBySearchFilterString(string filterString){
            var result = _userService.GetUsersBySearchFilterString(filterString);
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