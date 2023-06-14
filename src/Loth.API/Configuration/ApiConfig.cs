using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;

namespace Loth.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());                
            });

            return services;
        }

        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Development"); // Usar apenas nas demos => Configuração Ideal: Production
                app.UseHsts();
            }            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();


            return app;
        }
    }
}
