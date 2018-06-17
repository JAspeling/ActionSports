using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ActionSports.API.Repositories;
using ActionSports.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ActionSports.API {
    public class Startup {
        public Startup(IConfiguration configuration, ILoggerFactory factory) {
            Configuration = configuration;
            this.Logger = factory.CreateLogger<Startup>();

            SetScoresheetRepo();
        }

        private void SetScoresheetRepo() {
            string path = null;
            //try {
            //    path = ("LOCALAPPDATA");
            //    if (path == null) throw new Exception("Failed to retrieve the System Variable '%LOCALAPPDATA%'");
            //} catch (Exception ex) {
            //    ex.CustomLog(Logger);
            //}
            path = Path.Combine("ActionSportsScoresheets");
            Logger.LogDebug($"Using Repo Path: {path}");
            try {
                if (!Directory.Exists(path)) {
                    Logger.LogDebug($"Path does not exist, creating...");
                    Directory.CreateDirectory(path);
                }
            } catch (Exception ex) {
                ex.CustomLog(Logger, "Failed to create Scoresheet Repository Path");
            }
            AppState.ScoresheetRepo = path;
        }

        public IConfiguration Configuration { get; }
        public ILogger Logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options => {
                options.AddPolicy("AllowAll",
                    builder => {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddMvc();
            services.AddScoped<IVenuesRepository, VenuesRepository>();
            services.AddScoped<ILeaguesRepository, LeaguesRepository>();
            services.AddScoped<IStandingsRepository, StandingsRepository>();
            services.AddScoped<IScoresheetRepository, ScoresheetRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseMvc();
        }
    }
}
