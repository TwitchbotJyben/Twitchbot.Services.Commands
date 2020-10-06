using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Twitchbot.Common.Base.Client;
using Twitchbot.Common.Base.Middleware;
using Twitchbot.Common.Models.Data;
using Twitchbot.Common.Models.Domain.Mapping;
using Twitchbot.Services.Commands.Business;
using Twitchbot.Services.Commands.Controllers;
using Twitchbot.Services.Commands.Dao;
using Twitchbot.Services.Commands.Interfaces;

namespace Twitchbot.Services.Commands
{
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
            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry();

            // sharing the user secret configuration file
            var connectionString = Configuration.GetConnectionString("twitchbot-dev");

            services.AddDbContext<TwitchbotContext>(options => options.UseNpgsql(connectionString));

            // register AutoMapper profiles
            services.AddAutoMapper(typeof(TwitchProfile));

            services
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()) // register validators
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.AddScoped<CommandsController>();
            services.AddScoped<ICommandsBusiness, CommandsBusiness>();
            services.AddScoped<ISpotifyBusiness, SpotifyBusiness>();
            services.AddScoped<IUptimeBusiness, UptimeBusiness>();
            services.AddScoped<ClientBase>();
            services.AddScoped<CommandsDao>();

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Twitchbot.Services.Commands", Version = "v1" });
            });

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                    new CultureInfo("en-GB"),
                    new CultureInfo("en-US"),
                    new CultureInfo("en"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("fr"),
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en-GB");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui, specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Twitchbot.Services.Commands");
            });

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
        }
    }
}