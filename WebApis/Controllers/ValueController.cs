using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApis.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly ILogger<ValueController> _logger;

        public ValueController(ILogger<ValueController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetValues()
        {
            return Ok("Request successful!");
        }
        
        [HttpGet("WithRole")]
        [Authorize(Roles = "Value.Read")]
        public IActionResult GetValuesWithRole()
        {
            return Ok("Request successful!");
        }
    }
}