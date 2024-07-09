namespace clothes.api.Instrafructure.DesignPattern.Promotion
{
    public class FixedDiscountDecorator : DiscountDecorator
    {
        private readonly int _discountFixed;

        public FixedDiscountDecorator(IOrder order, int discountFixed) : base(order)
        {
            _discountFixed = discountFixed;
        }
        public override int? GetTotalPrice => base.GetTotalPrice - _discountFixed;
    }
}
