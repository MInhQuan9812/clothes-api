using clothes.api.Dtos.Carts;
using clothes.api.Instrafructure.Context;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;

namespace clothes.api.Instrafructure.Services.Paypal
{
    public class PaypalPaymentStrategy : IPaypalStrategy
    {
        private readonly IConfiguration _configuration;
        private PaypalService _paypalService;
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
                int amount = dto.Total;
                ApprovalUrl =await PayUsingPaypal(amount);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<string> GetApprovalUrl()
        {
            return ApprovalUrl;
        }

        public async Task<string> PayUsingPaypal(int amount)
        {
            try
            {
                string approvalUrl =await PaymentWithPaypal(amount);
            }catch(Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
            return null;
        }

        private async Task<string> PaymentWithPaypal(int amount)
        {
            int _amount = amount / 25000;
            string returnUrl = "https://localhost:7217/Payment/Success";
            string cancelUrl = "https://localhost:7217/Cart/";

            var createdPayment = await _paypalService.CreateOrderAsync(_amount, returnUrl, cancelUrl);

            string approvalUrl = createdPayment.links.FirstOrDefault(x => x.rel.ToLower() == "approval_url")?.href;

            return await Task.FromResult(approvalUrl);
        }
    }
}
