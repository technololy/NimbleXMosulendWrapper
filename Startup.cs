using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MosulendWrapper.Services;

namespace MosulendWrapper
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
            services.AddTransient<CustomDelegatingHandler>();
            services.AddControllers();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerDocumentation();
            services.AddHttpClient("MosulendWrapper", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("REST_API_URLs").GetSection("Mosulend").Value);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddHttpMessageHandler<CustomDelegatingHandler>();

            services.AddHttpClient("OpenAPI", c =>
            {
                c.BaseAddress = new Uri("https://samples.openweathermap.org/data/2.5/weather?q=London,uk&appid=b6907d289e10d714a6e88b30761fae22");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });


            services.AddSingleton<Interfaces.IDataService, DataService>();
            services.AddSingleton<Interfaces.IAPiService, APIService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=MosulendCustomer}/{action=GetCustomersExistenceByPhone}");
            });



            app.UseSwaggerDocumentation();

        }
    }
}
