using AutoMapper;
using AutoMapper.Configuration;
using FluentValidation.AspNetCore;
using MedChart.MappingConfiguration;
using MedChart.Repositories;
using MedChart.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedChart.Common.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static object ObjectToJsonConverter { get; private set; }

        public static void ConfigureBusinessServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            if (services != null)
            {
                // Repositories
                services.AddScoped<IBloodPressureRepository, BloodPressureRepository>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();

                // Services
                services.AddScoped<IBloodPressureService, BloodPressureService>();
            }
        }

        public static void ConfigureMappings(this IServiceCollection services)
        {
            if (services != null)
            {
                //Automap settings
                services.AddAutoMapper(typeof(MappingProfile));
            }
        }

        public static void ConfigureValidators(this IServiceCollection services)
        {
            // Fluent Validation activation
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {

                    var errors = (c.ModelState.Values.Where(v => v.Errors.Count > 0)
                         .SelectMany(v => v.Errors)
                         .Select(v => v.ErrorMessage)).ToList();

                    var httpContext = c.HttpContext;
                    var loggerFactory = httpContext.RequestServices.GetRequiredService<ILoggerFactory>();

                    return new BadRequestObjectResult("Validation Error");
                };
            });
        }
    }
}
