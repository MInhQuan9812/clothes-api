namespace clothes.api.Instrafructure.DesignPattern.Promotion
{
    public abstract class DiscountDecorator : IOrder
    {

        protected readonly IOrder _order;
        public DiscountDecorator(IOrder order)
        {
            _order = order; 
        }

        public virtual int? GetTotalPrice => _order.GetTotalPrice;
    }
}
