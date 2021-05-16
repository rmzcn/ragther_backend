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
    public class TOIController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IMapper _mapper;
        private ITagsOfInterestService _tagsOfInterestService;
        public TOIController(ILogger<UserController> logger, IMapper mapper, ITagsOfInterestService tagsOfInterestService){
            _logger = logger;
            _tagsOfInterestService = tagsOfInterestService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{requesterUserName}")]
        public ActionResult GetTagsOfInterests(string requesterUserName)
        {
            var result = _tagsOfInterestService.GetInterestedTags(requesterUserName);
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
        [Route("{requesterUserName}/set")]
        public ActionResult SetTagsOfInterests(string requesterUserName,string tagList)
        {
            var list = tagList.Split(" ").ToList();
            List<int> tagIdList = new List<int>();
            foreach (var tagId in list)
            {
                tagIdList.Add(Convert.ToInt32(tagId));
            }

            var result = _tagsOfInterestService.SetInterestedTags(requesterUserName,tagIdList);
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