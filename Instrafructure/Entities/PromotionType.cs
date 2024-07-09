namespace clothes.api.Instrafructure.Entities
{
    public class PromotionType : Entity<int>
    {
        public string Title { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    }
}
