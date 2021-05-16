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
    public class WorkerController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IMapper _mapper;
        private IWorkWithService _workWithService;
        public WorkerController(ILogger<UserController> logger, IMapper mapper, IWorkWithService workWithService){
            _logger = logger;
            _workWithService = workWithService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("accept")]
        public ActionResult AcceptWorkerOffer(int todoId, string workerUserName)
        {
            var result = _workWithService.AddWorker(todoId,workerUserName);
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