using puchalski.service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Net;

namespace puchalski.spotify.api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "puchalski.spotify.api", Version = "v1" });
            });

            services.AddHttpLogging(logging => {
                logging.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });

            services.AddAntiforgery();
            services.AddScoped<IRecommendationService, RecommendationService>();

            IdentityModelEventSource.ShowPII = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseSerilogRequestLogging();
            app.UseHttpLogging();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "puchalski.spotify.api v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors(policyBuilder => {
                policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
