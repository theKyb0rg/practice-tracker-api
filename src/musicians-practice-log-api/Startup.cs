using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PracticeLog.Models;
using Microsoft.EntityFrameworkCore;
using musicians_practice_log_api.Models;

namespace musicians_practice_log_api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PracticeLogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // add cross origin requests
            services.AddCors(options =>
            {
                // angular 2 local
                options.AddPolicy("Angular2Local", 
                    builder => builder/*.WithOrigins("http://localhost:3000")*/
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                );
            });

            // Add framework services.
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, PracticeLogContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors("Angular2Local");

            app.UseMvc();

            // seed method
            DbInitializer.Initialize(context);
        }
    }
}
