using Demo.ApplicationProcess.Data;
using Demo.ApplicationProcess.Data.Repositories;
using Demo.ApplicationProcess.Domain.Infrastructure;
using Demo.ApplicationProcess.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Demo.ApplicationProcess.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // db
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //in-mem 
                options.UseInMemoryDatabase("Demo.db");

                // errors
                if (Environment.IsDevelopment())
                {
                    options.EnableDetailedErrors();
                }
                else
                {
                    options.EnableSensitiveDataLogging();
                }

            });

            // services
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<INewsService, NewsService>();

            // actions
            services.AddControllers();

            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Demo API",
                    Version = "v1"
                });
            });

            // cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.SetIsOriginAllowed(a => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

        }

        public void Configure(IApplicationBuilder builder)
        {
            //dev error page
            if (Environment.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            } 

            // allow all cors
            builder.UseCors("AllowAll");

            // access root files
            builder.UseStaticFiles();

            // routes
            builder.UseRouting();

            // auth
            builder.UseAuthorization();

            // endpoints
            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Demo Api");
                });

                endpoints.MapControllers();
            });

            //swagger
            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API v1");
            });

        }

    }
}
