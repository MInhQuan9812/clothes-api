namespace clothes.api.Instrafructure.DesignPattern.Promotion
{
    public class PercentageDiscountDecorator :DiscountDecorator
    {
        private readonly int _discountPercentage;

        public PercentageDiscountDecorator(IOrder order, int discountPercentage) : base(order)
        {
            _discountPercentage = discountPercentage;
        }

        public override int? GetTotalPrice
        {
            get
            {
                if (base.GetTotalPrice.HasValue && _discountPercentage != null)
                {
                    double discount = _discountPercentage / 100.0;
                    return (int)(base.GetTotalPrice.Value * (1 - discount));
                }
                else
                {
                    // Trả về null hoặc giá trị mặc định tùy thuộc vào logic của bạn
                    return base.GetTotalPrice;
                }
            }
        }
    }
}
