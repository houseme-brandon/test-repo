using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using AccessControl.Controllers;
using AccessControl.Data;
using AccessControl.Services;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserIdentity.Models;

namespace AccessControl
{
    public class Startup
    {
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients.Get())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Resources.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.Resources.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                var appContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                appContext.Database.Migrate();
            }
        }


        public void ConfigureIdentityServer(IServiceCollection services)
        {
            const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=AccessControl;Trusted_Connection=True;MultipleActiveResultSets=true";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var cert = new X509Certificate2(Path.Combine(@"C:\Projects\houseme-apps\HouseMe\ArchiveProj\AccessControl", "damienbodserver.pfx"), "");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                config.Cookies.ExternalCookie = new CookieAuthenticationOptions { AuthenticationScheme = 
                IdentityServerConstants.ExternalCookieAuthenticationScheme, AutomaticAuthenticate = false, AutomaticChallenge = false }; })
                .AddEntityFrameworkStores<ApplicationDbContext>()
               
                .AddDefaultTokenProviders();            
            
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddIdentityServer()
                .AddOperationalStore(builder =>
                    builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)))
                .AddConfigurationStore(builder =>
                    builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)))
                .AddAspNetIdentity<ApplicationUser>()
                .AddSigningCredential(cert);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //To be added for security
            //services.AddMvc(options =>
            //{
            //    options.SslPort = 44321;
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});

            ConfigureIdentityServer(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

          
            app.UseDeveloperExceptionPage();

            InitializeDatabase(app);

            app.UseIdentity();
            app.UseIdentityServer();
            //Google Authentication
            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                DisplayName = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                ClientId = "417187595564-3eja53jecsvacmp0d63rdl38c7t8rine.apps.googleusercontent.com",
                ClientSecret = "vDGX8C3MB9NK51lKkOUwBXXo"
            });
            //Facebook Authentication
            app.UseFacebookAuthentication(new FacebookOptions
            {
                AuthenticationScheme = "Facebook",
                DisplayName = "Facebook",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                //Information Requesting
                //Scope = { "public_profle", "email"},

                AppId = "1199848133463789",
                AppSecret = "7ce5d4fac0af7ea98ff3f0392c50faf9"
            });
            //Twitter Authentication
            app.UseTwitterAuthentication(new TwitterOptions()
            {
                AuthenticationScheme = "Twitter",
                DisplayName = "Twitter",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                ConsumerKey = "",
                ConsumerSecret = ""
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!!");
            });

        }
    }

}
