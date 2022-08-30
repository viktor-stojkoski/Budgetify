namespace Budgetify.Api.Registers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Budgetify.Api.Settings;
    using Budgetify.Contracts.Settings;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerUI;

    public static partial class Register
    {
        private const string ApiVersion1_0 = "1.0";

        public static IServiceCollection RegisterSwagger(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            ISwaggerSettings swaggerSettings = new SwaggerSettings(configuration);

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(
                        name: ApiVersion1_0,
                        info: new OpenApiInfo
                        {
                            Title = swaggerSettings.Version1_0_Name,
                            Version = ApiVersion1_0
                        });

                    c.AddSecurityDefinition("Bearer", new()
                    {
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. <br>Example: <h4>Bearer <TOKEN_HERE></h4>",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "bearer"
                    });

                    c.AddSecurityRequirement(new()
                    {
                       {
                           new()
                           {
                               Reference = new()
                               {
                                   Type = ReferenceType.SecurityScheme,
                                   Id = "Bearer"
                               },

                           },
                           new List<string>()
                       }
                    });

                    c.DocInclusionPredicate((docName, apiDesc) =>
                    {
                        ApiVersionModel? apiVersionModel = ApiVersion(apiDesc.ActionDescriptor);

                        return apiVersionModel == null
                            || apiVersionModel.ImplementedApiVersions.Any(x => x.ToString() == docName);
                    });

                    c.EnableAnnotations();
                    c.ExampleFilters();
                    c.CustomSchemaIds(schemaIdSelector => schemaIdSelector.FullName);
                });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            ISwaggerSettings swaggerSettings =
                serviceScope.ServiceProvider.GetRequiredService<ISwaggerSettings>();

            app.UseSwagger(options => options.RouteTemplate = swaggerSettings.RouteTemplate);

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = swaggerSettings.RoutePrefix;
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint(swaggerSettings.Version1_0_JsonEndpointUrl, swaggerSettings.Version1_0_Name);
            });

            return app;
        }

        private static ApiVersionModel? ApiVersion(ActionDescriptor? actionDescriptor)
        {
            return actionDescriptor?
                .Properties
                .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
                .Select(kvp => kvp.Value as ApiVersionModel)
                .FirstOrDefault();
        }
    }
}
