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
        private readonly IRepository<ProductVariant> _productVariantRepo;
        private readonly IRepository<VariantValue> _variantValueRepo;
        private readonly IRepository<Option> _optionRepo;
        private readonly IRepository<OptionValue> _optionValueRepo;

        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(
            IRepository<Product> productRepo,
            IRepository<ProductVariant> productVariantRepo,
            IRepository<VariantValue> variantValueRepo,
            IRepository<Option> optionRepo,
            IRepository<OptionValue> optionValueRepo,
            DbContext dbContext,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : base(httpContextAccessor)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepo = productRepo;
            _dbContext = dbContext;
            _productVariantRepo = productVariantRepo;
            _variantValueRepo = variantValueRepo;
            _optionRepo = optionRepo;
            _optionValueRepo= optionValueRepo;
        }

        [HttpGet("getProductTopSales")]
        public IActionResult GetTopSales()
        {
            return Ok();
        }

        [HttpGet("getProductByProductId/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepo.GetQueryableNoTracking()
                                        .Include(x => x.ProductVariants)
                                        .ThenInclude(x => x.VariantValues)
                                        .ThenInclude(x=>x.OptionValue)
                                      .FirstOrDefault(x => x.Id == id && !x.IsDeleted)
                                      ?? throw new ApplicationException("Product doesn not exits");
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet("getAllProduct")]
        public IActionResult GetProduct()
        {
            var product = _productRepo.GetQueryableNoTracking()
                                     .Include(x => x.ProductVariants)
                                     .ThenInclude(x=>x.VariantValues)
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

                product = _productRepo.Insert(product);
                _productRepo.SaveChanges();

                foreach (var option in dto.OptionValues)
                {
                    _optionValueRepo.Insert(CreateOptionValue(option,product));
                }
                _optionValueRepo.SaveChanges();

                foreach(var variant in dto.ProductVariants)
                {
                    _productVariantRepo.Insert(CreateProductVariant(variant,product));
                }
                _productVariantRepo.SaveChanges();

                _unitOfWork.Complete();
            }
            return Ok(_mapper.Map<ProductDto>(product));
        }

        private OptionValue CreateOptionValue(CreateOptionValueDto option, Product product)
        {
            var optionValue = new OptionValue()
            {
                ProductId=product.Id,
                OptionId = option.OptionId,
                Value = option.Value,
                Thumbnail=option.Thumbnail
            };
            return optionValue;
        }

        private ProductVariant CreateProductVariant(CreateProductVariantDto variant,Product product)
        {
            var productVariant = new ProductVariant()
            {
                VariantName = variant.VariantName,
                ProductId = product.Id,
                Price=variant.Price,
                Quantity = variant.Quantity
            };

            foreach (var varientValue in variant.VarientValues)
            {
                var option =_optionRepo.GetQueryableNoTracking()
                    .FirstOrDefault(x => x.Name.Equals(varientValue.Option))
                        ?? throw new ApplicationException("Option is invalid");

                var optValue = _optionValueRepo.GetQueryableNoTracking()
                    .FirstOrDefault(x => x.Value.Equals(varientValue.Value)&&x.OptionId==option.Id && x.ProductId==product.Id)
                        ?? throw new ApplicationException("Option Value is invalid");

                productVariant.VariantValues.Add(new VariantValue()
                {
                    ProductId = product.Id,
                    ProductVariantId=productVariant.Id,
                    OptionId = option.Id,
                    OptionValueId = optValue.Id
                });
            }
            return productVariant;
        }
    }
}
