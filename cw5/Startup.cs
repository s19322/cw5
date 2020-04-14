using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw5.Middlewares;
using cw5.Models;
using cw5.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace cw5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStudentDbService, SqlServerStudentDbService>();
            services.AddControllers();
           
            //1.dodawanie dokumentacji
            
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Students App API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //2.Dodawanie dokumentacji
            app.UseSwagger();
            app.UseSwaggerUI(config=> {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "s19322 App API");
            });

            app.UseMiddleware<LoggingMiddleware>();
            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("You have to put index no.");
                    return;
                }

                string index = context.Request.Headers["Index"].ToString();
                var stud = new StudentIndexChecker();
                Student student = stud.CheckIndex(index);

                if (student== null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("we don't have student with this index no.");
                    return;
                }else
                    await context.Response.WriteAsync("student exists in database");



                await next();
            });


            app.UseHttpsRedirection();

            //middlewear-y tutaj ->
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
