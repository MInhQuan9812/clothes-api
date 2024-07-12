using clothes.api.Dtos.Carts;
using clothes.api.Instrafructure.Context;
using clothes.api.Instrafructure.DesignPattern.Facade;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;

namespace clothes.api.Instrafructure.Services.Paypal
{
    public class PaypalPaymentStrategy : IPaymentStrategy
    {
        private Working _working;

        private readonly IConfiguration _configuration;
        public string ApprovalUrl { get; set; }
        private readonly IRepository<Cart> _cartRepo;

        public PaypalPaymentStrategy(ClothesContext context, IConfiguration configuration,IRepository<Cart> cartRepo)
        {
            _configuration = configuration;
            _cartRepo = cartRepo;
        }

        public async Task<bool> Paymention(int userId, CheckOutDto dto)
        {
            try
            {
                var cart=_cartRepo.GetQueryableNoTracking().FirstOrDefault(x => x.CustomerId == userId) ?? throw new ApplicationException("Cart doesn not exits");
                int amount = dto.Total;
                ApprovalUrl =await PayUsingPaypal(amount);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public string GetApprovalUrl()
        {
            return ApprovalUrl;
        }

        public async Task<string> PayUsingPaypal(int amount)
        {
            try
            {
                string approvalUrl = await ServiceFacade.Instance(_configuration).PaymentWithPaypall(amount);
                if (!string.IsNullOrEmpty(approvalUrl))
                {
                    return approvalUrl;
                }
                else
                {
                    throw new Exception("Error to initial Payment");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

       
    }
}
