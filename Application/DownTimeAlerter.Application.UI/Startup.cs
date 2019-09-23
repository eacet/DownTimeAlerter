using DownTimeAlerter.Application.UI.Configuration;
using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.Domain.Context;
using DownTimeAlerter.Data.Domain.Entities;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DownTimeAlerter.Application.UI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //Sql Database
            services.AddDbContext<DownTimeAlerterDbContext>(config => {
                config.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            //Identity
            services.AddDefaultIdentity<User>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<DownTimeAlerterDbContext>();

            //hangfire Setup
            services.AddHangfire(options => {
                options.UseSqlServerStorage(Configuration.GetConnectionString("Default"));
            });


            services.RegisterDependencies(); //Service and Repository Injection


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {


            app.MigrateDb();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            //Hangfire Setup
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            //Hangfire Job Create
            RecurringJob.AddOrUpdate<IHangfireService>(
               options => options.CreateRecurringJobsForMonitorings(), Cron.Minutely);


            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Monitoring}/{action=Index}/{id?}");
            });


        }
    }
}
