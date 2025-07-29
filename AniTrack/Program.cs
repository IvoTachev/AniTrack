namespace AniTrack.Web
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Repository.Interface;
    using AniTrack.Data.Seeding;
    using AniTrack.Data.Seeding.Interfaces;
    using AniTrack.Services.Core.Interfaces;
    using AniTrack.Web.Infrastructure.Extensions;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services
                .AddDbContext<AniTrackDbContext>(options =>
                {
                options.UseSqlServer(connectionString);
                });
            
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<ApplicationUser>(options => 
                {
                    ConfigureIdentity(builder.Configuration, options);
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AniTrackDbContext>();

         
            builder.Services.AddRepositories(typeof(IAnimeRepository).Assembly);
            builder.Services.AddUserDefinedServices(typeof(IAnimeService).Assembly);
            builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            builder.Services.AddControllersWithViews();

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

            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.SeedDefaultIdentity();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
           

            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentity(IConfigurationManager configurationManager, IdentityOptions identityOptions)
        {
            identityOptions.Password.RequiredLength = configurationManager.GetValue<int>($"IdentityConfig:Password:RequiredLength");
            identityOptions.Password.RequireNonAlphanumeric = configurationManager.GetValue<bool>($"IdentityConfig:Password:RequireNonAlphanumeric");
            identityOptions.Password.RequireDigit = configurationManager.GetValue<bool>($"IdentityConfig:Password:RequireDigit");
            identityOptions.Password.RequireLowercase = configurationManager.GetValue<bool>($"IdentityConfig:Password:RequireLowercase");
            identityOptions.Password.RequireUppercase = configurationManager.GetValue<bool>($"IdentityConfig:Password:RequireUppercase");
            identityOptions.Password.RequiredUniqueChars = configurationManager.GetValue<int>($"IdentityConfig:Password:RequiredUniqueChars");
            identityOptions.SignIn.RequireConfirmedAccount = configurationManager.GetValue<bool>($"IdentityConfig:SignIn:RequireConfirmedAccount");
            identityOptions.SignIn.RequireConfirmedEmail = configurationManager.GetValue<bool>($"IdentityConfig:SignIn:RequireConfirmedEmail");
            identityOptions.SignIn.RequireConfirmedPhoneNumber = configurationManager.GetValue<bool>($"IdentityConfig:SignIn:RequireConfirmedPhoneNumber");
            identityOptions.User.RequireUniqueEmail = configurationManager.GetValue<bool>($"IdentityConfig:User:RequireUniqueEmail");

        }
    }
}
