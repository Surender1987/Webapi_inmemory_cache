using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Webapi_inmemory_cache.BusinessLayer;
using Webapi_inmemory_cache.BusinessLayer.Interfaces;
using Webapi_inmemory_cache.DataaccessLayer;
using Webapi_inmemory_cache.DataaccessLayer.Interfaces;
using Webapi_inmemory_cache.DataaccessLayer.Models;

namespace Webapi_inmemory_cache
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Student API", Version = "v1" });
            });

            services.AddMemoryCache();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<StudentDBContext>(ctx => ctx.UseInMemoryDatabase("database"));
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentDataAdapeter, StudentDataAdapeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API");
            });

            app.UseMvc();

        }
    }
}
