using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CouponManagerAPI.Controllers.API
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        public HealthCheckController()
        {
        }

        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok("I'm fine");
        }
    }
}