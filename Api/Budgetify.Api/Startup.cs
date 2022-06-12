namespace Budgetify.Api
{
    using Budgetify.Api.Registers;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHttpContextAccessor();

            services.AddControllers();

            services
                .RegisterSettings()
                .RegisterStorage()
                .RegisterDatabase(Configuration)
                .RegisterCommands()
                .RegisterQueries()
                .RegisterDomainEvents()
                .RegisterHangfire(Configuration)
                .RegisterLogging()
                .RegisterLoggingServices()
                .RegisterHealthChecks(Configuration)
                .RegisterSwagger(Configuration)
                .RegisterServices()
                .RegisterAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSwagger();

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseCors("AllowAll");

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            //app.UseHangfire();

            //app.UseHealthChecks();

            app
                .UseSwagger()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseCors("AllowAll")
                .UseAuthentication()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseHangfire()
                .UseHealthChecks();
        }
    }
}
