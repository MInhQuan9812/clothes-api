using AutoMapper;
using clothes.api.Common.Seedworks;
using clothes.api.Dtos.Carts;
using clothes.api.Instrafructure.Contant;
using clothes.api.Instrafructure.Context;
using clothes.api.Instrafructure.DesignPattern.Promotion;
using clothes.api.Instrafructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Repositories
{
    public class CartWorking
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClothesContext _context;
        private Working _working;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<Promotion> _promotionRepo;
        private readonly IRepository<Order> _orderRepo;

        public CartWorking(ClothesContext context, IConfiguration configuration, IRepository<Cart> cartRepo, IRepository<Promotion> promotionRepo, IRepository<Order> orderRepo)
        {
            _context = context;
            _configuration = configuration;
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
            _promotionRepo=promotionRepo;
            _working = new Working(_context, _configuration, _cartRepo, _promotionRepo, _orderRepo);
        }


        public Order CheckOut(IUnitOfWork unitOfWork,string paymentUrl, int cartId, int userId, [FromBody] CheckOutDto value)
        {
            var cart = _cartRepo
               .GetQueryable()
               .Include(x => x.CartItems)
               .ThenInclude(cartItem => cartItem.ProductVariant)
               .FirstOrDefault(x => x.Id == cartId && x.CustomerId == userId && !x.IsDeleted)
                    ?? throw new ApplicationException("Cart does not exist");

            var order = new Order
            {
                CustomerId = userId,
                Address = value.Address,
                Total = value.Total,
                PaymentId = value.PaymentId,
                PromotionId = value.PromotionId,
                PaymentUrl = paymentUrl
            };

            IOrder discountedOrder;
            var discountValue = _promotionRepo.GetQueryableNoTracking().Where(x => x.Id == value.PromotionId).FirstOrDefault();

            discountedOrder = discountValue.PromotionValue < Contants.ProportionDiscount
                ? new PercentageDiscountDecorator(order, discountValue.PromotionValue)
                : new FixedDiscountDecorator(order, discountValue.PromotionValue);

            order.Total = discountedOrder.GetTotalPrice;

            using (unitOfWork.Begin())
            {
                foreach (var lineItem in cart.CartItems)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductVariantId = lineItem.ProductVariantId,
                        Quantity = lineItem.Quantity,
                        ProductId = lineItem.ProductVariant.ProductId,
                        TotalPrice = (int)(lineItem.ProductVariant.Price * lineItem.Quantity),
                    };
                    order.OrderDetails.Add(orderDetail);
                }
                _cartRepo.SaveChanges();
                _orderRepo.Insert(order);
                unitOfWork.Complete();
            }
            return order;
        }
    }
}
