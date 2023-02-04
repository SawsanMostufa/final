using ECommerice.Api.Helpers;
using ECommerice.Core.IRepository;
using ECommerice.Core.IUniteOfWork;
using ECommerice.Infrastructure.Repository;
using ECommerice.Infrastructure.UniteOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.Extensions.Helpers
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddAppServices (this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IOrderRepo, OrderRepo>(); 
            services.AddScoped<IUniteOfWork, UniteOfWork>();
            services.AddScoped<IUploaderRepo, UploaderRepo>();            

            //Configuration validation errors as global
            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
