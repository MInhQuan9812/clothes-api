using clothes.api.Dtos.Carts;

namespace clothes.api.Instrafructure.Services.Paypal
{
    public interface IPaymentStrategy
    {
        Task<bool> Paymention(int amount, CheckOutDto dto);

        string GetApprovalUrl();
    }
}
