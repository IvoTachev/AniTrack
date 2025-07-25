
namespace AniTrack.Data.Repository.Interface
{
    using AniTrack.Data.Models;

    public interface IAnimeRepository 
        : IRepository<Anime, int>, IAsyncRepository<Anime,int>
    {

    }
}
