using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CouponManagerAPI.Infrastructure;
using Consul;
using Swashbuckle.AspNetCore.Swagger;

namespace CouponManagerAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("inm"));

            //services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));
            //services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            //{
            //    var address = Configuration["consulConfig:address"];
            //    consulConfig.Address = new Uri(address);
            //}));
            // Add framework services.
            services.AddMvc();

            //services.AddSwaggerGen(opt =>
            //{
            //    opt.SwaggerDoc("doc", new Info() { Title = "CouponManager" });
            //});

            //services.AddServiceDiscovery(Configuration.GetSection("CouponManager"));

            services.AddCors(
           options => options.AddPolicy("AllowCors",
               builder => {
                   builder
                    
                       .AllowAnyOrigin() //AllowAllOrigins;  
                  
                   .WithMethods("GET", "PUT", "POST", "DELETE") //AllowSpecificMethods;  
                                                                //.AllowAnyMethod() //AllowAllMethods; 
                   //.WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header"); //AllowSpecificHeaders;  
                   .AllowAnyHeader(); //AllowAllHeaders;  
                })
       );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApiContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            //var context = app.ApplicationServices.GetService<ApiContext>();
            AddSampleData(context);

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/doc/swagger.json", "CouponManager API");
            //});

            app.UseMvc();
            app.UseCors("AllowCors");
            //app.UseConsulRegisterService();
        }

        private static void AddSampleData(ApiContext context)
        {            
            context.Campaign.AddRange(
                     Enumerable.Range(1, 10).Select(t => new Campaign { CampaignName = "Monsoon offer"+t, CampaignDescription = "Monsoon offer" + t})
                 );

            context.Coupon.AddRange(
                Enumerable.Range(0, 9).Select(t => new Coupon { CampaignId = t+1, CouponsCount = 1 + t, CouponDiscount = 5 + t, CouponDiscountAmount = 200 + t, CouponExpirationDate = DateTime.Now.AddDays(t), CouponNumber = "MONSOON " + t, CouponTitle = "Monsoon offer Flat 1" + t + "%" })
            );           
          

            //Save data into in-memory database
            context.SaveChanges();
        }
    }
}
