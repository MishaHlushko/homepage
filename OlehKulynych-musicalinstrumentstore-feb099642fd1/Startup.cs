using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Musical_Instrument_Store.Data.Repository;
using Musical_Instrument_Store.Data.Services;
using Musical_Instrument_Store.Data.Models.UserAppDBContext;
using Microsoft.AspNetCore.Identity;

namespace Musical_Instrument_Store
{
    public class Startup
    {
        private IConfigurationRoot _confString;
        private IConfigurationRoot _confStringDB;

        public Startup(IHostEnvironment hostingEnvironment)
        {
            _confString = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("appsettings.json").Build();
            _confStringDB = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<UserAppDBContext>(options => options.UseSqlServer(_confStringDB.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(_confStringDB.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserAppDBContext>();


            services.AddScoped<IMusicalInstrumentRepository, MusicalInstrumentRepository>();
            services.AddScoped<IMICategoryRepository, MICategoryRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IMusicalInstrumentService, MusicalInstrumentService>();
            services.AddScoped<IMICategoryService, MICategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped(sp => CartRepository.GetCartAsync(sp));

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMemoryCache();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseSession();
            app.UseAuthentication();
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitializeIdentity(serviceProvider);
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    AppDBContext appDBContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            //    DBObjects.Initial(appDBContext);
            //}
        }

        private void InitializeIdentity(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            Task<IdentityResult> roleResultAdmin;
            var admin = _confString.GetSection("Admin");

            string adminEmail = admin.GetValue<string>("email");
            string adminPassword = admin.GetValue<string>("password");

            //Check that there is an Administrator role and create if not
            Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Admin");
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResultAdmin = roleManager.CreateAsync(new IdentityRole("Admin"));
                roleResultAdmin.Wait();
            }
            //Check if the admin user exists and create it if not
            //Add to the Administrator role

            Task<User> adminUser = userManager.FindByEmailAsync(adminEmail);
            adminUser.Wait();

            if (adminUser.Result == null)
            {
                User administrator = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Name = "Admin",
                    Surname = "Admin",
                    Address = "Address",
                    PhoneNumber = "380000000000"
                };

                Task<IdentityResult> newUser = userManager.CreateAsync(administrator, adminPassword);
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Admin");
                    newUserRole.Wait();
                }
            }
        }

    }
}
