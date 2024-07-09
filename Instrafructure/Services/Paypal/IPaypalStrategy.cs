using clothes.api.Dtos.Carts;

namespace clothes.api.Instrafructure.Services.Paypal
{
    public interface IPaypalStrategy
    {
        Task<bool> Paymention(int amount, CheckOutDto dto);

        Task<string> GetApprovalUrl();
    }
}
