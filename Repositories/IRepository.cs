namespace clothes.api.Repositories
{
    public interface IRepository<TEntity>
    {

        TEntity Find(int? key);

        TEntity Insert(TEntity entity);

        TEntity Update(int key, TEntity entity);


        void Delete(int key);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetQueryableNoTracking();
        IQueryable<TEntity> GetQueryable();

        void SaveChanges();
    }
}
