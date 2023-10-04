using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Dotnet7.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Dotnet7
{
    public class Startup
    {
        //private static string localConnectionString = "Data Source=localhost; Initial Catalog=TestDB; uid=sa; password=P@ssw0rd; Connect Timeout=30000; TrustServerCertificate=false; Encrypt=false;";
        private static string localConnectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=K@buto123;";
        private readonly static string ConfigurationFilePath = @"C:\Config\Configuration.xml";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static string connString
        {
            get
            {
               
                return localConnectionString;

            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDirectoryBrowser();

            #region Session setup
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromHours(24);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;


                // You might want to only set the application cookies over a secure connection:
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                //options.Cookie.SameSite = SameSiteMode.Strict;


            });
            #endregion

            //cshtml to be able to use session
            services.AddHttpContextAccessor();


            //services.AddDbContext<TestContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings"))); // Use the appropriate connection string
            services.AddDbContext<TestContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"))); // Use the appropriate connection string


            services.AddControllersWithViews().AddRazorRuntimeCompilation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configure error handling middleware for production
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Add middleware to serve static files (e.g., CSS, JavaScript)
            app.UseStaticFiles();

            // Add routing middleware before the EndpointMiddleware
            app.UseRouting();

            // Add authentication middleware (if needed)
            //app.UseAuthentication();

            //// Add authorization middleware (if needed)
            //app.UseAuthorization();

            // Add EndpointMiddleware to handle endpoint routing and execution
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // Add additional endpoint mapping as needed
            });
        }
    }
}
