using Microsoft.OpenApi.Models;

namespace Store.API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentaion(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreAPI",Version = "v1"});
                var secutitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer schema. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                };
                options.AddSecurityDefinition("bearer", secutitySchema);
                var securityRequirments = new OpenApiSecurityRequirement
                {
                   {secutitySchema,new[]{"bearer"} }
                };
                options.AddSecurityRequirement(securityRequirments);

            });
            return services;

        }
    }
}
