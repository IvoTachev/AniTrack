namespace AniTrack.Data.Seeding
{
    using AniTrack.Data.Models;
    using AniTrack.Data.Seeding.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using static GCommon.ApplicationConstants;
    public class IdentitySeeder : IIdentitySeeder
    {
        private readonly string[] DefaultRoles = new[]
        {
            AdminRoleName,
            UserRoleName
        };
        
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly IUserEmailStore<ApplicationUser> emailStore;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        public IdentitySeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userStore = userStore;
            this.emailStore = this.GetEmailStore();
            this.configuration = configuration;
        }
        public async Task SeedIdentityAsync()
        {
            await this.SeedRolesAsync();
            await this.SeedUsersAsync();
        }

        public async Task SeedRolesAsync()
        {
         
            foreach (string role in DefaultRoles)
            {
                bool roleExists = await this.roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    IdentityRole identityRole = new IdentityRole(role);
                    IdentityResult result = await this.roleManager.CreateAsync(identityRole);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Failed to create role {role}");
                    }
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            string? testUserEmail = this.configuration["UserSeed:TestUser:Email"];
            string? testUserPassword = this.configuration["UserSeed:TestUser:Password"];
            string? testUserName = this.configuration["UserSeed:TestUser:Username"];
            string? adminUserEmail = this.configuration["UserSeed:TestAdmin:Email"];
            string? adminUserPassword = this.configuration["UserSeed:TestAdmin:Password"];
            string? adminUserName = this.configuration["UserSeed:TestAdmin:Username"];

            // Explicit IDs for seeding
            string testUserId = this.configuration["UserSeed:TestUser:Id"] ?? "test-user-id";
            string adminUserId = this.configuration["UserSeed:TestAdmin:Id"] ?? "admin-user-id";

            if (testUserEmail == null || testUserPassword == null || testUserName == null ||
               adminUserEmail == null || adminUserPassword == null || adminUserName == null)
            {
                throw new InvalidOperationException("User seed configuration is missing or incomplete.");
            }
            ApplicationUser? userSeeded = await this.userManager.FindByNameAsync(testUserName);
            if (userSeeded == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Id = testUserId // Set explicit Id
                };

                await this.userStore.SetUserNameAsync(user, testUserName, CancellationToken.None);
                await this.emailStore.SetEmailAsync(user, testUserEmail, CancellationToken.None);
                IdentityResult result = await this.userManager.CreateAsync(user, testUserPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to create user {testUserName}");
                }
                result = await this.userManager.AddToRoleAsync(user, UserRoleName);
            }
            ApplicationUser? adminSeeded = await this.userManager.FindByNameAsync(adminUserName);
            if (adminSeeded == null)
            {
                ApplicationUser adminUser = new ApplicationUser
                {
                    Id = adminUserId // Set explicit Id
                };

                await this.userStore.SetUserNameAsync(adminUser, adminUserName, CancellationToken.None);
                await this.emailStore.SetEmailAsync(adminUser, adminUserEmail, CancellationToken.None);
                IdentityResult result = await this.userManager.CreateAsync(adminUser, adminUserPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to create user {adminUserName}");
                }
                result = await this.userManager.AddToRoleAsync(adminUser, AdminRoleName);
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)userStore;
        }
    }
}
