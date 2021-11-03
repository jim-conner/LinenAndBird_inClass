using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LinenAndBird_inClass
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
            //registering a service - test ASP.NET  how to build a thing
            //services.AddTransient<IConfiguration>() -> create a new thing anytime someone asks for one
            //services.AddScoped<IConfiguration>() create new thing once per http req
            services.AddSingleton<IConfiguration>(Configuration);
            //...AddSingleton<> is giving back same copy of what you ask for ... forever until app stops running
            services.AddTransient<BirdRepository>();
            services.AddTransient<HatRepository>();
            services.AddTransient<OrdersRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.IncludeErrorDetails = true;
                   options.Authority = "https://securetoken.google.com/fish-store-a71e6";
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateLifetime = true,
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidAudience = "fish-store-a71e6",
                       ValidIssuer = "https://securetoken.google.com/fish-store-a71e6"
                   };
               });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinenAndBird_inClass", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinenAndBird_inClass v1"));
            }

            app.UseCors(cfg => cfg.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
