using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace DroneGuardSystem
{
    public class Startup
    {
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Makes it possible to use controllers.
            services.AddControllers();

            services.AddHealthChecks(checks =>
            {
                /*
                 * url or ip to drone. Drone guard system will request to see if it gets a response "200 OK"
                 *
                 * the second parameter is cacheDuration. Changed to 1 millisecond from 5 minute. 5 minute is the
                 * default
                 */
                checks.AddUrlCheck("http://localhost:6789", TimeSpan.FromMilliseconds(1));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            

            //makes it possible to expose endpoints in the controller.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });
        }
    }
}