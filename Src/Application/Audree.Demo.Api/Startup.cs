using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Audree.Demo.Infrastructure.Data;
using AutoMapper;
using Audree.Demo.Core.Contracts.IUnitOfWork;
using Audree.Demo.Infrastructure.Unit_Of_Work;
using Audree.Demo.Infrastructure.Repositories.Admin;
using Audree.Demo.Core.Contracts.IRepositories.Admin;
using Audree.Demo.Api.Helpers;

namespace Audree.Demo.Api
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
            #region Global Cors policy
            services.AddCors(options =>
            {
                options.AddPolicy("m",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
            });
            #endregion


            services.AddControllers();
            #region Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PL Clubs", Version = "v1" });

                //First we define the security scheme
                //c.AddSecurityDefinition("Bearer", //Name the security scheme
                //    new OpenApiSecurityScheme
                //    {
                //        Description = "JWT Authorization header using the Bearer scheme.",
                //        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                //        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                //    });
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                //    {
                //        new OpenApiSecurityScheme{
                //            Reference = new OpenApiReference{
                //                Id = "Bearer", //The name of the previously defined security scheme.
                //                Type = ReferenceType.SecurityScheme
                //            }
                //        },new List<string>()
                //    }
                //});
            });
            #endregion
            services.AddDbContext<Plcontextclass>(item => item.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region configure DI for application services
            // configure DI for application services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPlclubRepository, PlClubRepository>();
            services.AddScoped<IEmployRepository, EmployRepository>();
            #endregion
            #region Auto Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperClass());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region Enable middleware to serve generated Swagger as a JSON endpoint.
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            #endregion
            #region Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),specifying the Swagger JSON endpoint.
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audree");
                //c.RoutePrefix = string.Empty;
            });
            #endregion
            app.UseCors("m");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
