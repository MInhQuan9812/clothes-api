using AutoMapper;
using clothes.api.Dtos.Carts;
using clothes.api.Dtos.Wishlist;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Controllers
{
    [Route("api/Wishlist")]
    [ApiController]
    public class StoreWishlistController : BaseController
    {
        private readonly IRepository<Wishlist> _wishlistRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public StoreWishlistController(
            IRepository<Wishlist> wishlistRepo,
            IRepository<Product> productRepo,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(httpContextAccessor)
        {
            _wishlistRepo = wishlistRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet("getWishlistByUserId/{id}")]
        public IActionResult GetWishlistsByUserId(int id)
        {
            var wishlist = _wishlistRepo.GetQueryableNoTracking()
                                .Include(x => x.User)
                                .Include(x => x.WishlistItems)
                                .ThenInclude(x => x.Product)
                                .FirstOrDefault(x => x.UserId == id && !x.IsDeleted)
                                ?? throw new ApplicationException("Wishlist does not exist");
            return Ok(_mapper.Map<WishlistDto>(wishlist));
        }
        [HttpPost("createWishlistById{id}")]
        public IActionResult CreateWishlistByUserId(int id)
        {
            if (_wishlistRepo.GetQueryableNoTracking().Where(x => x.UserId == id && !x.IsDeleted).Any())
            {
                throw new ApplicationException("This customer already had a wishlist");
            }
            return Ok(_mapper.Map<WishlistDto>(_wishlistRepo.Insert(new Wishlist(id))));
        }

        [HttpPost("addWishlistItem")]
        public IActionResult AddWishlistItem(int id, [FromBody] AddToWishListDto dto)
        {
            var wishlist = _wishlistRepo.GetQueryable().Include(x => x.WishlistItems)
                                            .FirstOrDefault(x => x.Id == id && !x.IsDeleted) ?? throw new ApplicationException("Wishlist does not exist");


            if (dto.ProductId== null)
            {
                throw new ApplicationException("ProductId must not empty");
            }
            var product = _productRepo
                                .GetQueryableNoTracking()
                                .FirstOrDefault(x => x.Id == dto.ProductId && !x.IsDeleted)
                                ?? throw new ApplicationException("Product does not exist");

            var wishlistItem = wishlist.WishlistItems.FirstOrDefault(x => x.ProductId.Equals(product.Id));

            if (wishlistItem == null)
            {
                wishlistItem = new WishlistItem(dto.ProductId);
                wishlist.WishlistItems.Add(wishlistItem);
                wishlist.LastUpdate = DateTime.Now;
            }
            else
            {
                wishlistItem.LastUpdate = DateTime.Now;
            }
            wishlist.LastUpdate = DateTime.Now;
            _wishlistRepo.SaveChanges();
            return Ok(_mapper.Map<WishlistItemDto>(wishlistItem));
        }

        [HttpDelete("{id}/wishlistItems/{wishlistItemId}")]
        public IActionResult DeleteWishlistItemItem(int id, int wishlistItemId, [FromBody] AddToWishListDto value)
        {
            var wishlist = _wishlistRepo
                .GetQueryable()
                .Include(x => x.WishlistItems)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted)
               ?? throw new ApplicationException("Wishlist does not exist");

            var wishlistItem = wishlist.WishlistItems.FirstOrDefault(x => x.Id == wishlistItemId)
                ?? throw new ApplicationException("Wishlist item does not exist");

            wishlist.WishlistItems.Remove(wishlistItem);
            wishlist.LastUpdate = DateTime.Now;
            _wishlistRepo.SaveChanges();
            return Ok();
        }
    }
}
