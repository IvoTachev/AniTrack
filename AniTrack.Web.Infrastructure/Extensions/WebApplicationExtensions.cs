namespace AniTrack.Web.Infrastructure.Extensions
{
    using AniTrack.Data.Seeding.Interfaces;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder SeedDefaultIdentity(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            IIdentitySeeder identitySeeder = serviceProvider.GetRequiredService<IIdentitySeeder>();

            identitySeeder.SeedIdentityAsync().GetAwaiter().GetResult();

            return app;
        }

        public static IApplicationBuilder SeedDefaultReviews(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            IReviewSeeder reviewSeeder = serviceProvider.GetRequiredService<IReviewSeeder>();

            reviewSeeder.SeedReviewAsync().GetAwaiter().GetResult();

            return app;
        }
    }
}
