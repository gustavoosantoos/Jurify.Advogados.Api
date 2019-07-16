﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;
using System.Linq;

namespace Jurify.Advogados.Api.Infraestrutura.Configuracoes
{
    public static class Swagger
    {
        public static void AdicionarDocumentacaoSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocument(config =>
            {
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                config.AddSecurity("JWT", Enumerable.Empty<string>(),
                     new SwaggerSecurityScheme()
                     {
                         Type = SwaggerSecuritySchemeType.ApiKey,
                         Name = "Authorization",
                         In = SwaggerSecurityApiKeyLocation.Header,
                         Description = "Copy this into  the value field: \nBearer {my long token}"
                     }
                );
            });
        }

        public static void UsarDocumentacaoSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUi3();
        }
    }
}
