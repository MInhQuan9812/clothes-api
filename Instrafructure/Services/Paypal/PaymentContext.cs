using clothes.api.Dtos.Carts;

namespace clothes.api.Instrafructure.Services.Paypal
{
    public class PaymentContext
    {
        private readonly IPaymentStrategy _paymentStrategy;

        public PaymentContext(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public Task<bool> ProcessOrderPayment(int userId, CheckOutDto checkoutDto)
        {
            return _paymentStrategy.Paymention(userId, checkoutDto);
        }
    }
}
