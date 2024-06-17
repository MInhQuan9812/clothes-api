using clothes.api.Instrafructure;
using clothes.api.Instrafructure.Context;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ClothesContext _context;
        public Repository(ClothesContext context) 
        {
            _context = context;
        }
        private DbSet<TEntity> _entity => _context.Set<TEntity>();
        public virtual IQueryable<TEntity> GetQueryableNoTracking() => _entity.AsNoTracking();
        public virtual IQueryable<TEntity> GetQueryable() => _entity.AsQueryable();

        public virtual TEntity Find(int? key)
        {
            return _entity.Find(key);
        }
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity is IHasCreationTime)
            {
                var hasCreationTimeEntity = (IHasCreationTime)entity;
                hasCreationTimeEntity.CreateTime = DateTime.Now;
            }
            _entity.Add(entity);
            _context.Entry(entity).State = EntityState.Added;

            SaveChange(entity);
            return entity;
        }

        public virtual TEntity Update(int key, TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if(entity is ILastUpdatedTime)
            {
                var lastUpdatedTime=(ILastUpdatedTime)entity;
                lastUpdatedTime.LastUpdate=DateTime.Now;
            }

            _context.Entry(entity).State=EntityState.Modified;
            SaveChange(entity);
            return entity;
        }

        public virtual void Delete(int key)
        {
            var dbEntity = _entity.Find(key);
            if (dbEntity == null)
                throw new NullReferenceException();

            if (dbEntity is IDeleteEntity)
            {
                var deletedEntity = (IDeleteEntity)dbEntity;
                deletedEntity.IsDeleted = true;
            }
            _context.Entry(dbEntity).State = EntityState.Modified;
            SaveChange(dbEntity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException();

            if (entity is IDeleteEntity)
            {
                var deletedEntity = (IDeleteEntity)entity;
                deletedEntity.IsDeleted = true;
            }
            _context.Entry(entity).State = EntityState.Modified;
            SaveChange(entity);
        }

        protected virtual void SaveChange(TEntity entity)
        {
            _context.SaveChanges();
        }
        protected virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        void IRepository<TEntity>.SaveChanges()
        {
            SaveChanges();
        }
    }
}
