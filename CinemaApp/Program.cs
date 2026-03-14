using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web
{
    using CinemaApp.Data;
    using CinemaApp.Data.Repository;
    using CinemaApp.Data.Repository.Contracts;
    using CinemaApp.Services.Core;
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Services.Mapping;
    using CinemaApp.Services.Models.Movie;
    using CinemaApp.Web.Infrastructure.Extensions;
    using CinemaApp.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<CinemaAppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            AutoMapperConfig.RegisterMappings(typeof(MovieAllDto).Assembly, typeof(AllMoviesIndexViewModel).Assembly);

            builder.Services.RegisterRepositories(typeof(MovieRepository));

            builder.Services.RegisterUserServices(typeof(MovieService));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => 
            { 
              ConfigureIdentity(builder.Configuration, options);
            })
                .AddEntityFrameworkStores<CinemaAppDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton(AutoMapperConfig.MapperInstance);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
        private static void ConfigureIdentity(ConfigurationManager configuration, IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = configuration
                .GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

            options.SignIn.RequireConfirmedEmail = configuration
                .GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");

            options.SignIn.RequireConfirmedPhoneNumber = configuration
             .GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            options.Password.RequireDigit = configuration
             .GetValue<bool>("Identity:Password:RequireDigit");

            options.Password.RequireLowercase = configuration
           .GetValue<bool>("Identity:Password:RequireLowercase");

            options.Password.RequiredLength = configuration
            .GetValue<int>("Identity:Password:RequiredLength");

            options.Password.RequiredUniqueChars = configuration
            .GetValue<int>("Identity:Password:RequiredUniqueChars");

            options.Password.RequireNonAlphanumeric = configuration
            .GetValue<bool>("Identity:Password:RequireNonAlphanumeric");

            options.Password.RequireUppercase = configuration
            .GetValue<bool>("Identity:Password:RequireUppercase");
        }
    }

}
