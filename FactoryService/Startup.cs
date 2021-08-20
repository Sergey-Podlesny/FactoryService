using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryService.Models;

namespace FactoryService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            DataBase dataBase = new DataBase { ServerName = "localhost", DbName = "AdoDB" };
            services.AddMvc();
            
            if(!dataBase.IsExist())
            {
                dataBase.CreateDb();
            }
            dataBase.CheckTable(new Company());
            dataBase.CheckTable(new Employee());


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}"
                    );
            });
        }
    }
}
