using clothes.api.Instrafructure.Context;
using clothes.api.Instrafructure.Entities;

namespace clothes.api.Repositories
{
    public class Working
    {
        private readonly IConfiguration _configuration;
        private readonly ClothesContext _context;
        private CartWorking _cartWorking;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<Promotion> _promotionRepo;
        private readonly IRepository<Order> _orderRepo;
        public Working(ClothesContext context, IConfiguration configuration, IRepository<Cart> cartRepo, IRepository<Promotion> promotionRepo, IRepository<Order> orderRepo)
        {
            _context = context;
            _configuration = configuration;
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
            _promotionRepo = promotionRepo;
        }


        public CartWorking cartWorking
        {
            get
            {

                if(_cartWorking == null )
                {
                    _cartWorking = new CartWorking(_context,_configuration,_cartRepo,_promotionRepo,_orderRepo);
                }
                return _cartWorking;    
            }
        }

    }
}
