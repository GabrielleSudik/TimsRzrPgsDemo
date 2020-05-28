using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RzrPgsDemoApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //This is where all the services get injected into the whole app.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            //you can add different things here, depending on what you need. We need a singleton.
            //other options are Scope and Transient, which work better in diff kinds of apps. 
            //ConnectionStringData is a class we created in our Common / DB project.
            services.AddSingleton(new ConnectionStringData
            {
                SqlConnectionName = "Default" //we manually configure this property
                                              //but you an automatically configure instead.
            });

            //add more - all items from the Common / DB project.
            services.AddSingleton<IDataAccess, SqlDb>(); //these services we are not wiring up manually.
            services.AddSingleton<IFoodData, FoodData>();
            services.AddSingleton<IOrderData, OrderData>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting(); //sets us up to use routing

            app.UseAuthorization();

            app.UseEndpoints(endpoints => //details of routing here
            {
                endpoints.MapRazorPages(); //check for the "Pages" folder in the solution.
                                           //this method treats Pages as the root, and referenes to other pages
                                           //in that file. Ex: "/Error" refers to Pages/Error.cshtml
                                           //see above at line "app.UseExceptionHandler("/Error");"
                                           //that means, when that method is called, route to Error page.

                //other types of projects will have different endpoints.MapStuff methods.
            });
        }
    }
}
