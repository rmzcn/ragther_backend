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
        [Route("{userName}/profile/{requesterUserName}")]
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
                    return NotFound(Messages.UserNotFound);
                }
                else if (result.Message == Messages.UsersAreNotFriends)
                {
                    return Unauthorized();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register(VMUserRegisterPost model){
            var result = _userService.Register(model);
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
        [HttpPost("login")]
        public ActionResult Login(VMUserLoginPost model){
            var result = _userService.Login(model.UserName, model.Password);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return Unauthorized(result.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("search/{filterString}")]
        public ActionResult GetUsersBySearchFilterString(string filterString){
            var result = _userService.GetUsersBySearchFilterString(filterString);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        

    }
}