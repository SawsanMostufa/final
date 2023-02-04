using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.Helpers.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSWaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerice.Api", Version = "v1" });
                var securitSchema = new OpenApiSecurityScheme
                {
                    Description = "JWT auth bearer schema",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitSchema);
                var securityRequirement = new OpenApiSecurityRequirement {{ securitSchema, new[] { "Bearer" }}};
                c.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerice.Api v1"));

            return app;
        }
    }
}
