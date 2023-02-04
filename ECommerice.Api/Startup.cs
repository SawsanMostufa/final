using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ECommerice.Infrastructure;
using AutoMapper;
using ECommerice.Api.Helpers;
using ECommerice.Api.Extensions.Helpers;
using ECommerice.Api.Helpers.Extensions;
using ECommerice.Api.Helpers.Middleware;
using StackExchange.Redis;

namespace ECommerice.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddControllers();

            //config db context
            services.AddDbContext<StoreContext>(x => 
                x.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));


            //config identity
            //services.AddDbContext<AppIdentityDbContext>(x =>
            //{
            //    x.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            //});

            //config redis connection
            //services.AddSingleton<IConnectionMultiplexer>(c =>
            //{
            //    var config = ConfigurationOptions
            //        .Parse(_configuration.GetConnectionString("Redis"),true);
            //    return ConnectionMultiplexer.Connect(config);
            //});

            services.AddAppServices();
            services.AddIdentityServices(_configuration);

            services.AddSWaggerDoc();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorePolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "http://localhost:4201");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            //app.UseDeveloperExceptionPage();

            app.UseSwaggerDoc();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("CorePolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
