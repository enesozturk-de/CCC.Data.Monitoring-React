using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Controllers;
using CCC.Data.Monitoring.Data.Access;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Data.Access.EFCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CCC.Data.Monitoring
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

            services.AddControllersWithViews();

            services.AddDbContext<MonitoringDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "CCCMonitoring"));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMonitorDataRepository, MonitorDataRepository>();
            services.AddScoped<IQueueGroupRepository, QueueGroupRepository>();


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MonitoringDbContext context, IAccountRepository accountRepository,
                                IMonitorDataRepository monitorDataRepository, IQueueGroupRepository queueGroupRepository)
        {
            if (env.IsDevelopment())
            { 
                DataGenerator dataGenerator = new DataGenerator(accountRepository, monitorDataRepository, queueGroupRepository);
                dataGenerator.GenerateData(); 

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
