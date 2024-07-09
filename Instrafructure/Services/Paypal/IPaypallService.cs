using PayPal.Api;

namespace clothes.api.Instrafructure.Services.Paypal
{
    public interface IPaypallService
    {
        Task<Payment> CreateOrderAsync(int amount, string returnUrl, string cancelUrl);
    }
}
