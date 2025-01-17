﻿
using Abackend.Data;
using Abackend.Repositories.Implementation;
using Abackend.Repositories.Interface;
using Backend.Errors;
using Core.Interface;
using Infrastructure.Data;
using Infrastructure.GenericImplementation;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Backend.Extensions {
    public static class ApplicationServicesExtensions {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config) {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddDbContext<ApplicationDbContext>(options =>

                options.UseSqlServer(config.GetConnectionString("CodePConnectionString")));


            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
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


            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
            return services;
        }
     }
}
