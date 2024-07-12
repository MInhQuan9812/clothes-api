using clothes.api.Dtos.Carts;
using clothes.api.Instrafructure.Context;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;

namespace clothes.api.Instrafructure.Services.Paypal
{
    public class DirectPaymentStrategy : IPaymentStrategy
    {
        private Working _working;
        private readonly ClothesContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<Promotion> _promotionRepo;
        private readonly IRepository<Order> _orderRepo;

        public DirectPaymentStrategy(ClothesContext context, IConfiguration configuration,IRepository<Cart> cartRepo)
        {
            _context = context;
            _configuration = configuration;
            _cartRepo = cartRepo;

        }

        public async Task<bool> Paymention(int userId, CheckOutDto checkoutDto)
        {
            try
            {
                //_working.cartWorking.CheckOut(userId, userId, checkoutDto);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public string GetApprovalUrl()
        {
            return null;
        }
    }
}
