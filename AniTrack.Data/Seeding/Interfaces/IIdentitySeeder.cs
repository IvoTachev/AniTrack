namespace AniTrack.Data.Seeding.Interfaces
{
    public interface IIdentitySeeder
    {
        // Method to seed identity data, such as roles and default users.
        Task SeedIdentityAsync();
    }
}
