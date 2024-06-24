using AutoMapper;
using clothes.api.Common.Seedworks;
using clothes.api.Dtos.Options;
using clothes.api.Dtos.Product;
using clothes.api.Dtos.Products;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace clothes.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IRepository<Product> _productRepo;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(
            IRepository<Product> productRepo,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : base(httpContextAccessor)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("getProductTopSales")]
        public IActionResult GetTopSales()
        {
            return Ok();
        }

        [HttpGet("getProductById/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepo.GetQueryableNoTracking()
                                        .Include(x => x.ProductOptionValues)
                                        .ThenInclude(x => x.OptionValue)
                                      .FirstOrDefault(x => x.Id == id && !x.IsDeleted)
                                      ?? throw new ApplicationException("Product doesn not exits");
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet("getAllProduct")]
        public IActionResult GetProduct()
        {
            var product = _productRepo.GetQueryableNoTracking()
                                     .Include(x => x.ProductOptionValues)
                                     .ThenInclude(x => x.OptionValue)
                                      .Where(x => !x.IsDeleted);
            return Ok(_mapper.Map<ICollection<ProductDto>>(product));
        }


        [HttpPost("createProduct")]
        public IActionResult CreateProduct([FromBody] CreateProductDto dto)
        {
            var product = new Product()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Thumbnail = dto.Thumbnail,
            };
            using (_unitOfWork.Begin())
            {
                if (_productRepo.GetQueryableNoTracking().
                    FirstOrDefault(x => x.Id.Equals(product.Id)) != null)
                    throw new ApplicationException("Product is already exist.");


                foreach (var optionItem in dto.OptionValues)
                {
                    product.ProductOptionValues.Add(CreateProductOptionValue(optionItem, product.Id));
                }
                product = _productRepo.Insert(product);
                _productRepo.SaveChanges();
                _unitOfWork.Complete();
            }
            return Ok(_mapper.Map<ProductDto>(product));
        }

        private ProductOptionValue CreateProductOptionValue(CreateOptionValueDto optionItem, int id)
        {
            var optionValue = new OptionValue()
            {
                OptionId = optionItem.OptionId,
                Value = optionItem.Value,
                Price = optionItem.Price,
                Thumbnail=optionItem.Thumbnail,
            };
            var productOptionValue = new ProductOptionValue()
            {
                OptionId = optionItem.OptionId,
                OptionValue = optionValue,
                ProductId = id,
            };
            return productOptionValue;
        }
    }
}
