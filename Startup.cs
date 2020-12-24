using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FitBananas.Models;
using Microsoft.EntityFrameworkCore;

namespace FitBananas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ApiHelper.InitializeClient();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BananaContext>(options => options.UseMySql (Configuration["DBInfo:ConnectionString"]));
            services.AddSession();
            services.AddMvc(options => options.EnableEndpointRouting = false);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            // Middleware to allow protect the site from users trying to bypass Strava authentication
            app.Use(async (context, next) =>
            {
                // Figure out how to allow post request from the strava database only.
                // there should be a value in the response that will tell us where the information is coming from.
                // It should be in the context.request.headers
                if(context.Request.Method == "Post")
                {
                    
                }
                // if a user is not signed in, they will be redirected back to the splash page
                else if(context.Request.Path != "" && context.Session.GetInt32("UserId") == null)
                {
                    // reassigns the path to "/" before passing it back to the controller in "app.UseMvc"
                    context.Request.Path = "/";
                    Console.WriteLine($"Attempted server breach averted at {DateTime.Now}");
                }
                await next();

            });
            app.UseMvc();

            // app.UseRouting();

            // app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllerRoute(
            //         name: "default",
            //         pattern: "{controller=Home}/{action=Index}/{id?}");
            // });
        }
    }
}
