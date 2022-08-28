using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi_11
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer Scheme \r\n\r\n" +
                "Enter 'Bearer' [Space] and  then your Token in the text input below \r\n\r\n" +
                "Example:Bearer 12345abeder \r\n\r\n"+
                "Name:Authorization \r\n" +
                "In:header",

                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            }); ;
            //xx
            options.AddSecurityRequirement(new OpenApiSecurityRequirement());

        }
    }
    
}
