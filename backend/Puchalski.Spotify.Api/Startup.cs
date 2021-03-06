using MediatR;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Puchalski.Spotify.Api.Configuration;
using Puchalski.Spotify.Application.Configuration;
using Puchalski.Spotify.Domain.Recommendation;
using Puchalski.Spotify.Domain.Search;
using Puchalski.Spotify.ExternalApi.Abstraction;
using Puchalski.Spotify.ExternalApi.Main;
using Serilog;
using System.Net;

namespace Puchalski.Spotify.Api {
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

            (string client_id, string client_secret) = getConfiguration(Configuration);

            services.AddAntiforgery();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddScoped<IExternalCommonApi>(_ => new SpotifyApi(client_id, client_secret));
            services.AddAutoMapper(typeof(ApplicationProfile).Assembly);
            services.AddAutoMapper(typeof(DomainProfile).Assembly);
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            IdentityModelEventSource.ShowPII = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Serilog.ILogger logger) {
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
            app.ConfigureExceptionHandler(logger);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

        private (string client_id, string client_secret) getConfiguration(IConfiguration _configuration) {
            string? recommendationApiName = _configuration.GetRequiredSection("RecommendationApiName")?.Value;
            if (string.IsNullOrEmpty(recommendationApiName)) {
                throw new Exception("RecommendationApiName required");
            }
            var recommendationApiSpotifySection = _configuration.GetRequiredSection("RecommendationApiSpotify");
            if (!recommendationApiSpotifySection.Exists()) {
                throw new Exception("RecommendationApiSpotify required");
            } else {
                if (string.IsNullOrEmpty(recommendationApiSpotifySection["client_id"])) {
                    throw new Exception("RecommendationApiName client_id required");
                }

                if (string.IsNullOrEmpty(recommendationApiSpotifySection["client_secret"])) {
                    throw new Exception("RecommendationApiName client_secret required");
                }

                return (recommendationApiSpotifySection["client_id"], recommendationApiSpotifySection["client_secret"]);
            }
        }
    }
}
