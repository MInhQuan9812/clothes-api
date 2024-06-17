namespace clothes.api.Instrafructure.Entities
{
    public class Payment : Entity<int>
    {

        public string Title { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
