namespace AniTrack.Data.Repository.Interface
{
    using System.Linq;

    public interface IRepository<TEntity, TKey>
    {
        TEntity? GetById(TKey id);

        TEntity? SingleOrDefault(Func<TEntity, bool> predicate);

        TEntity? FirstOrDefault(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetAllAttached();

        int Count();
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        bool Delete(TEntity entity);    

        bool HardDelete(TEntity entity);

        bool Update(TEntity entity);

        void SaveChanges();
    }
}
