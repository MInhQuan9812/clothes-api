using AutoMapper;
using clothes.api.Dtos.Product;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IRepository<Product> _productRepo;

        private readonly IMapper _mapper;


        public ProductController(
            IRepository<Product> productRepo,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet("topSales")]
        public IActionResult GetTopSales()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepo.GetQueryableNoTracking()
                                    .Include(x => x.Category)
                                     .Include(x => x.ProductOptionValues)
                                      .ThenInclude(x => x.OptionValue).ThenInclude(x => x.Option)
                                      .FirstOrDefault(x => x.Id == id && !x.IsDeleted)
                                      ?? throw new ApplicationException("Product doesn not exits");
            return Ok(_mapper.Map<ProductDto>(product));                                     
        }
    }
}
