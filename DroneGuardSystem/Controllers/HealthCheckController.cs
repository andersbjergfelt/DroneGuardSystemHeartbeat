using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.HealthChecks;

namespace DroneGuardSystem.Controllers
{
    
    [Route("/[controller]")]
    public class HealthCheckController : Controller
    {
        private readonly IHealthCheckService _healthCheckService;

        public HealthCheckController(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           
            var healthCheckResult = await _healthCheckService.CheckHealthAsync();

            if (healthCheckResult.CheckStatus == CheckStatus.Healthy)
            {
                return new JsonResult(new {Drone = "I am a healthy drone"});
            }
            
            var droneDown = healthCheckResult.CheckStatus != CheckStatus.Healthy;

            if (droneDown)
            {
                var failedHealthCheckDescriptions = healthCheckResult.Results.Where(r => r.Value.CheckStatus != CheckStatus.Healthy)
                    .Select(r => r.Value.Data.Values.First())
                    .ToList();
                return new JsonResult(new {Error = "Drone 1 is not responding"}){ StatusCode = StatusCodes.Status500InternalServerError };
            }

            return Ok();
        }
    }
}