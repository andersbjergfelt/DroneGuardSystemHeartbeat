using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.HealthChecks;

namespace DroneGuardSystem.Controllers
{
    //this endpoint is exposed /healthcheck 
    //full url to access this is: localhost:5000/healthcheck
    [Route("/[controller]")]
    public class HealthCheckController : Controller
    {
        //Inject in an IHealthCheckService so we can call CheckHealthAsync further down
        private readonly IHealthCheckService _healthCheckService;

        public HealthCheckController(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           //run a single Health Check it makes a call to the drone to check if everything is fine
           //await means it is an async action.
           /*
            * We get back a CompositeHealthCheckResult which is a summary of the health check
            * that we registered in theAddHealthChecks method in our Startup class.
            * That class has a CheckStatus property that has 4 options (Healthy, Unhealthy, Warning, and Unknown).
            * We have chosen to check if the Drone is Healthy
            * 
            * 
            */
           var healthCheckResult = await _healthCheckService.CheckHealthAsync();

            if (healthCheckResult.CheckStatus == CheckStatus.Healthy)
            {
                return new JsonResult(new {Drone = "I am a healthy drone 1"});
            }
            
            
            var droneDown = healthCheckResult.CheckStatus != CheckStatus.Healthy;

            //if the drone does not return 200 OK. 
            if (droneDown)
            {
                /*
                 * Ring the alarm here.
                 */
                
                return new JsonResult(new {Error = "Drone 1 is not responding"}){ StatusCode = StatusCodes.Status500InternalServerError };
            }

            //returns 200 OK
            return Ok();
        }
    }
}