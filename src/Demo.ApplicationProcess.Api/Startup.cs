using Demo.ApplicationProcess.Data;
using Demo.ApplicationProcess.Data.Repositories;
using Demo.ApplicationProcess.Domain.Infrastructure;
using Demo.ApplicationProcess.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

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
    
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

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
            builder.UseForwardedHeaders();
            
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
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    context.Response.ContentType = "text/plain";

                    // Host info
                    var name = Dns.GetHostName(); // get container id
                    var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                    Console.WriteLine($"Host Name: { System.Environment.MachineName} \t {name}\t {ip}");
                    await context.Response.WriteAsync($"Host Name: {System.Environment.MachineName}{System.Environment.NewLine}");
                    await context.Response.WriteAsync(System.Environment.NewLine);

                    // Request method, scheme, and path
                    await context.Response.WriteAsync($"Request Method: {context.Request.Method}{System.Environment.NewLine}");
                    await context.Response.WriteAsync($"Request Scheme: {context.Request.Scheme}{System.Environment.NewLine}");
                    await context.Response.WriteAsync($"Request URL: {context.Request.GetDisplayUrl()}{System.Environment.NewLine}");
                    await context.Response.WriteAsync($"Request Path: {context.Request.Path}{System.Environment.NewLine}");

                    // Headers
                    await context.Response.WriteAsync($"Request Headers:{System.Environment.NewLine}");
                    foreach (var (key, value) in context.Request.Headers)
                    {
                        await context.Response.WriteAsync($"\t {key}: {value}{System.Environment.NewLine}");
                    }
                    await context.Response.WriteAsync(System.Environment.NewLine);

                    // Connection: RemoteIp
                    await context.Response.WriteAsync($"Request Remote IP: {context.Connection.RemoteIpAddress}");
                });
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
