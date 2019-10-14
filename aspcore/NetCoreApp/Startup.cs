using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Enter once in start
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseBadRequestHandler();

            app.Map("/home", home =>
            {
                home.Map("/index", Index);
                home.Map("/about", About);

                home.Run(async context =>
                {
                    await context.Response.WriteAsync("Home page");
                });
            });

            app.Map("", home =>
            {
                home.Run(async context =>
                {
                throw new Exception("Incorrect request");
                    //await context.Response.WriteAsync("Start page");
                });
            });

            app.Run(async (context) =>
            {
                if (context.Response.StatusCode == 404)
                    await context.Response.WriteAsync("<h1 style=\"color:red\">Page not found</h1>");
            });
        }

        private static void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Index page");
            });
        }
        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("About page");
            });
        }
    }
}
