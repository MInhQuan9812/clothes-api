namespace clothes.api.Instrafructure
{
    public abstract class Entity : IDeleteEntity, IHasCreationTime, ILastUpdatedTime
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdate { get; set; }


        public Entity()
        {
            CreateTime = DateTime.Now;
        }
    }

    public abstract class Entity<TKey> : Entity
    {
        public TKey Id { get; set; }
    }
}
