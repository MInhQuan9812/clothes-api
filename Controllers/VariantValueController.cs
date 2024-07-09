using AutoMapper;
using clothes.api.Common.Seedworks;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace clothes.api.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class VariantValueController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<VariantValue> _variantValueRepo;
        private readonly IRepository<OptionValue> _optionValueRepo;
        public VariantValueController(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IRepository<VariantValue> variantValueRepo,
    IHttpContextAccessor httpContextAccessor
    ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _variantValueRepo = variantValueRepo;
            _unitOfWork = unitOfWork;
        }
        //[HttpGet]
        //public IActionResult GetProductVariantFromOptionValue()
        //{
        //    var productVariant=_optionValueRepo.
        //    return Ok();
        //}

    }
}
