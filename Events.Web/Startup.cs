using AutoMapper;
using Events.Core.Common;
using Events.Core.Common.Messages;
using Events.Core.Common.Validators;
using EventsManager.Data;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;

namespace Events.Web
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

            services.AddDbContext<EventsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
            //    >((ServiceProvider) =>
            //{
            //    var env=ServiceProvider.GetRequiredService<Iwe>
            //});



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
