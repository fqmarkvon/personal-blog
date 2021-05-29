using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Microsoft.EntityFrameworkCore.Design;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Identity;



namespace Blog
{
    public class Startup
    {
        
           private IConfiguration _config;
           public Startup(IConfiguration config) {
               _config = config;
           }

           // This method gets called by the runtime. Use this method to add services to the container.
           // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
           public void ConfigureServices(IServiceCollection services)
           {
               services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config["DefaultConnection"]));

               services.AddIdentity<IdentityUser,IdentityRole>(options => {

                   options.Password.RequireDigit = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequiredLength = 6;
               })
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

               services.AddTransient<IRepository, Repository>();

               services.AddMvc(options =>options.EnableEndpointRouting=false);
           }
   
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseMvcWithDefaultRoute();
            //app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseMvcWithDefaultRoute();

//            app.UseEndpoints(endpoints =>
 //           {
                /* endpoints.MapGet("/", async context =>
                 {
                     await context.Response.WriteAsync("Hello World!");
                 });*/
  //              endpoints.MapDefaultControllerRoute();
   //         });
        }
    }
}
