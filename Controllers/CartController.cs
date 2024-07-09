using AutoMapper;
using clothes.api.Common.Seedworks;
using clothes.api.Dtos.Carts;
using clothes.api.Instrafructure.DesignPattern.Promotion;
using clothes.api.Instrafructure.Entities;
using clothes.api.Instrafructure.Services.Paypal;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop.Infrastructure;
using Newtonsoft.Json.Linq;

namespace clothes.api.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<OptionValue> _optionValueRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<Promotion> _promotionRepo;
        private readonly IRepository<ProductOptionValue> _productOptionValueRepo;
        private readonly IRepository<ProductVariant> _productVariantRepo;
        private readonly IRepository<VariantValue> _variantValueRepo;
        private readonly IPaypalStrategy _paypalStrategy;

        public CartController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<Cart> cartRepo,
            IRepository<User> userRepo,
            IRepository<OptionValue> optionValueRepo,
            IRepository<VariantValue> variantValueRepo,
            IRepository<Order> orderRepo,
            IRepository<Promotion> promotionRepo,
            IPaypalStrategy paypalStrategy,
            IRepository<ProductVariant> productVariantRepo,
            IHttpContextAccessor httpContextAccessor
            ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _cartRepo = cartRepo;
            _optionValueRepo = optionValueRepo;
            _variantValueRepo = variantValueRepo;
            _userRepo = userRepo;
            _orderRepo= orderRepo;
            _promotionRepo = promotionRepo;
            _paypalStrategy = paypalStrategy;
            _productVariantRepo = productVariantRepo;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetCartByUserId/{id}")]
        public IActionResult GetCartByUserId(int id)
        {
            var cart = _cartRepo.GetQueryableNoTracking()
                                .Include(x => x.Customer)
                                .Include(x => x.CartItems)
                                .ThenInclude(x => x.ProductVariant)
                                .ThenInclude(x => x.VariantValues)
                                .ThenInclude(x => x.OptionValue)
                                .FirstOrDefault(x => x.CustomerId == id && !x.IsDeleted)
                                ?? throw new ApplicationException("Cart does not exist");
            return Ok(_mapper.Map<CartDto>(cart));
        }

        //[HttpPost("createCart")]
        //public IActionResult CreateCart()
        //{
        //    if (_cartRepo.GetQueryableNoTracking().Where(x => x.CustomerId == LoggingUserId && !x.IsDeleted).Any())
        //    {
        //        throw new ApplicationException("This customer already had a cart");
        //    }
        //    return Ok(_mapper.Map<CartDto>(_cartRepo.Insert(new Cart(LoggingUserId))));
        //}

        [HttpPost("createCartById{id}")]
        public IActionResult CreateCartByUserId(int id)
        {
            if (_cartRepo.GetQueryableNoTracking().Where(x => x.CustomerId == id && !x.IsDeleted).Any())
            {
                throw new ApplicationException("This customer already had a cart");
            }
            return Ok(_mapper.Map<CartDto>(_cartRepo.Insert(new Cart(id))));
        }

        [HttpPost("AddCartItem")]
        public IActionResult AddCartItem(int id, [FromBody] AddLineItemDto dto)
        {
            var cart = _cartRepo.GetQueryable().Include(x => x.CartItems)
                                            .FirstOrDefault(x => x.Id == id && !x.IsDeleted) ?? throw new ApplicationException("Cart does not exist");


            if (dto.ProductVariantId == null)
            {
                throw new ApplicationException("ProductVarientId must not empty");
            }

            if (dto.Quantity <= 0)
            {
                throw new ApplicationException("Quantity must be greater than 0");
            }

            var productVarient = _productVariantRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Id == dto.ProductVariantId && !x.IsDeleted)
                    ?? throw new ApplicationException("Product varient does not exist");

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductVariantId.Equals(productVarient.Id));

            if (cartItem == null)
            {
                cartItem = new CartItem(dto.ProductVariantId, dto.Quantity);
                cart.CartItems.Add(cartItem);
                cart.LastUpdate = DateTime.Now;
            }
            else
            {
                cartItem.Quantity += dto.Quantity;
                cartItem.LastUpdate = DateTime.Now;
            }
            cart.LastUpdate = DateTime.Now;
            _cartRepo.SaveChanges();
            return Ok(_mapper.Map<CartItemDto>(cartItem));
        }

        [HttpPut("{id}/lineItems/{lineItemId}")]
        public IActionResult UpdateLineItem(int id, int lineItemId, [FromBody] AddLineItemDto value)
        {
            var cart = _cartRepo
                .GetQueryable()
                .Include(x => x.CartItems)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted)
               ?? throw new ApplicationException("Cart does not exist");

            if (value.ProductVariantId==null)
                throw new ApplicationException("ProductVarientId must not empty");

            if (value.Quantity <= 0)
                throw new ApplicationException("Quantity must be greater than 0");

            var productVarient = _productVariantRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Id.Equals(value.ProductVariantId) && !x.IsDeleted)
                    ?? throw new ApplicationException("Product varient does not exist");

            var cartItem = cart.CartItems.FirstOrDefault(x => x.Id == lineItemId)
                ?? throw new ApplicationException("Line item does not exist");

            cartItem.LastUpdate = DateTime.Now;
            cartItem.Quantity = value.IsIncreasedBy
                ? cartItem.Quantity += value.Quantity
                : cartItem.Quantity = value.Quantity;

            
            _cartRepo.Update(id, cart);
            _cartRepo.SaveChanges();

            return Ok(_mapper.Map<CartItemDto>(cartItem));
        }

        [HttpDelete("{id}/lineItems/{lineItemId}")]
        public IActionResult DeleteLineItem(int id, int lineItemId, [FromBody] AddLineItemDto value)
        {
            var cart = _cartRepo
                .GetQueryable()
                .Include(x => x.CartItems)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted)
               ?? throw new ApplicationException("Cart does not exist");

            var lineItem = cart.CartItems.FirstOrDefault(x => x.Id == lineItemId)
                ?? throw new ApplicationException("Line item does not exist");

            cart.CartItems.Remove(lineItem);
            cart.LastUpdate = DateTime.Now;
            _cartRepo.SaveChanges();

            return Ok();
        }

        [HttpDelete("deleteCart/{id}")]
        public IActionResult RemoveCart(int id)
        {
            var cart = _cartRepo.GetQueryable().Include(x => x.CartItems).FirstOrDefault(x => x.Id == LoggingUserId && !x.IsDeleted)
                ?? throw new ApplicationException("Cart doesn not exist");

            _cartRepo.Delete(cart);
            return Ok();
        }

        [HttpPost("{cartId}/CheckOut")]
        public IActionResult CheckOut(int cartId, int userId, [FromBody] CheckOutDto value)
        {
            var cart = _cartRepo
               .GetQueryable()
               .Include(x => x.CartItems)
               .ThenInclude(cartItem => cartItem.ProductVariant)
               .FirstOrDefault(x => x.Id == cartId && x.CustomerId == userId && !x.IsDeleted)
                    ?? throw new ApplicationException("Cart does not exist");

           
            //var customer = _userRepo
            //    .GetQueryableNoTracking()
            //    .FirstOrDefault(x => x.Id == Id)
            //        ?? throw new AppException("User does not exist");

            //var deliveryAddress = _deliveryAddressRepo
            //    .GetQueryableNoTracking()
            //    .FirstOrDefault(x => x.Id == value.DeliveryAddressId)
            //        ?? throw new AppException("Delivery address does not exist");

            var order = new Order
            {
                CustomerId = userId,
                Address = value.Address,
                Total = value.Total,
                PaymentId = value.PaymentId,
                PromotionId=value.PromotionId              
            };

            IOrder discountedOrder;
            var discountValue = _promotionRepo.GetQueryableNoTracking().Where(x => x.Id == value.PromotionId).FirstOrDefault();
            if (discountValue.PromotionValue < 100)
            {
                discountedOrder = new PercentageDiscountDecorator(order, discountValue.PromotionValue);
            }
            else
            {
                discountedOrder = new FixedDiscountDecorator(order, discountValue.PromotionValue);
            }
            order.Total = discountedOrder.GetTotalPrice;

            using (_unitOfWork.Begin())
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
                _unitOfWork.Complete();
            }
            return Ok(order);
        }

    }
}
