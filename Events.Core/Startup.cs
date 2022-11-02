using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Events.Core.Common;
using Events.Core.Common.Validators;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Events.Core.Common.Messages;
using Events.Core.Controllers;
using Events.Core.Common.Helpers;

namespace EventsManager
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
            services.AddControllers();
            services.AddDataProtection();
            services.AddControllersWithViews();


            //DbContextOptions<DbContext> contextOptions = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase("Context").Options; services.AddSingleton(contextOptions);

            services.AddDbContext<EventsContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                   // options.EnableSensitiveDataLogging();
            //        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); ;
                });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSingleton(provider =>

             new MapperConfiguration(config =>
             {
                 config.AddProfile(new AutoMapperProfiles());
             }).CreateMapper()
             );

            //add a validator per controller
            services.AddSingleton<IDataValidator, DataValidator>();
            services.AddSingleton<IMessages, En_Messages>();
            services.AddSingleton<IHelper, Helper>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Events", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {   new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events v1"));

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller}/{action=Index}/{id?}");

            });
            if (!env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<EventsContext>();
                    context.Database.Migrate();
                }
            };
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.Options.SourcePath = "ClientApp";


                    //   spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");

                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
