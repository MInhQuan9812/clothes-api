using AutoMapper;
using clothes.api.Common.Seedworks;
using clothes.api.Dtos.Carts;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

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
        private readonly IRepository<ProductOptionValue> _productOptionValueRepo;

        public CartController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<Cart> cartRepo,
            IRepository<User> userRepo,
            IRepository<ProductOptionValue> productOptionValueRepo,
            IHttpContextAccessor httpContextAccessor
            ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _cartRepo = cartRepo;
            _userRepo = userRepo;
            _productOptionValueRepo = productOptionValueRepo;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetCartByUserId/{id}")]
        public IActionResult GetCartByUserId(int id)
        {
            var cart = _cartRepo.GetQueryableNoTracking()
                                .Include(x => x.Customer)
                                .Include(x => x.CartItems)
                                .ThenInclude(x => x.ProductOptionValue)
                                .FirstOrDefault(x => x.CustomerId == id && !x.IsDeleted)
                                ?? throw new ApplicationException("Cart does not exist");
            return Ok(_mapper.Map<CartDto>(cart));
        }

        [HttpPost("createCart")]
        public IActionResult CreateCart()
        {
            if (_cartRepo.GetQueryableNoTracking().Where(x => x.CustomerId == LoggingUserId && !x.IsDeleted).Any())
            {
                throw new ApplicationException("This customer already had a cart");
            }
            return Ok(_mapper.Map<CartDto>(_cartRepo.Insert(new Cart(LoggingUserId))));
        }

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
            if (dto.ProductVarientId == null)
            {
                throw new ApplicationException("ProductOptionValueId must not empty");
            }

            if (dto.Quantity <= 0)
            {
                throw new ApplicationException("Quantity must be greater than 0");
            }

            var productOptionValue = _productOptionValueRepo.GetQueryableNoTracking()
                                                            .FirstOrDefault(x => x.Id.Equals(dto.ProductVarientId) && !x.IsDeleted)
                                                            ?? throw new ApplicationException("ProductOptionValue does not exist");
            var cartItem=cart.CartItems.FirstOrDefault(x=>x.ProductOptionValueId.Equals(productOptionValue.Id));

            if (cartItem == null)
            {
                cartItem = new CartItem(dto.ProductVarientId, dto.Quantity);
                cart.CartItems.Add(cartItem);
                cart.LastUpdate = DateTime.Now;
            }
            else
            {
                cartItem.Quantity += dto.Quantity;
                cartItem.LastUpdate=DateTime.Now;
            }
            cart.LastUpdate = DateTime.Now;
            _cartRepo.SaveChanges();
            return Ok(_mapper.Map<CartItemDto>(cartItem));
        }


        [HttpDelete("deleteCart/{id}")]
        public IActionResult RemoveCart(int id)
        {
            var cart = _cartRepo.GetQueryable().Include(x => x.CartItems).FirstOrDefault(x => x.Id == LoggingUserId && !x.IsDeleted)
                ??throw new ApplicationException("Cart doesn not exist");

            _cartRepo.Delete(cart);
            return Ok();
        }


    }
}
