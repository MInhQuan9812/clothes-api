﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace clothes.api.Common
{
    internal class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly IConfiguration _configuration;
        public AuthorizeCheckOperationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)?.OfType<AuthorizeAttribute>().Any() ?? false
                || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (!hasAuthorize) return;

            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };


        }
    }
}
