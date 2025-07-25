namespace AniTrack.Data.Repository
{
    using AniTrack.Data.Repository.Interface;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using static Common.ExceptionMessages;

    public abstract class BaseRepository<TEntity, TKey> 
        : IRepository<TEntity, TKey>, IAsyncRepository<TEntity,TKey>
        where TEntity : class
    {
        protected readonly AniTrackDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        protected BaseRepository(AniTrackDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>();
        }


        public TEntity? GetById(TKey id)
        {
            return this.dbSet
                .Find(id);
        }

        public ValueTask<TEntity?> GetByIdAsync(TKey id)
        {
            return this.dbSet
                .FindAsync(id);
        }

        public TEntity? SingleOrDefault(Func<TEntity, bool> predicate)
        {
            return this.dbSet
                .SingleOrDefault(predicate);
        }

        public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet
                .SingleOrDefaultAsync(predicate);
        }
        public TEntity? FirstOrDefault(Func<TEntity, bool> predicate)
        {
            return this.dbSet
                .FirstOrDefault(predicate);
        }

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.dbSet
                .ToArray();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            TEntity[] entities = await this.dbSet.ToArrayAsync();

            return entities;
        }


        public int Count()
        {
            return this.dbSet
                .Count();
        }

        public Task<int> CountAsync()
        {
            return this.dbSet
                .CountAsync();
        }

        public IQueryable<TEntity> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public void Add(TEntity entity)
        {
           this.dbSet.Add(entity);
           this.dbContext.SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
           await this.dbSet.AddAsync(entity);
           await this.dbContext.SaveChangesAsync();
        } 

         public void AddRange(IEnumerable<TEntity> entities)
        {
            this.dbSet.AddRange(entities);
            this.dbContext.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await this.dbSet.AddRangeAsync(entities);
            await this.dbContext.SaveChangesAsync();
        }
        public bool Delete(TEntity entity)
        {
            this.PerformSoftDeleteOfEntity(entity);

            return this.Update(entity);
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            this.PerformSoftDeleteOfEntity(entity);

            return this.UpdateAsync(entity);
        }

        public bool HardDelete(TEntity entity)
        {
            this.dbSet.Remove(entity);
            int updCount = this.dbContext.SaveChanges();
            return updCount > 0;
        }

        public async Task<bool> HardDeleteAsync(TEntity entity)
        {
            this.dbSet.Remove(entity);
            int updCount = await this.dbContext.SaveChangesAsync();
            return updCount > 0;
        }
        public bool Update(TEntity entity)
        {
            try
            {
                this.dbSet.Attach(entity);
                this.dbSet.Entry(entity).State = EntityState.Modified;
                this.dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                this.dbSet.Attach(entity);
                this.dbSet.Entry(entity).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
     
        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        private PropertyInfo? GetIsDeletedProperty(TEntity entity)
        {
            return typeof(TEntity)
                .GetProperties()
                .FirstOrDefault(pi => pi.PropertyType == typeof(bool) &&
                                pi.Name == "IsDeleted");
        }

        private void PerformSoftDeleteOfEntity(TEntity entity)
        {
            PropertyInfo? isDeletedProperty =
                this.GetIsDeletedProperty(entity);
            if(isDeletedProperty == null)
            {
                throw new InvalidOperationException(SoftDeleteOfNonSoftDeletableEntity);
            }

            isDeletedProperty.SetValue(entity, true);
        }

    }
}
