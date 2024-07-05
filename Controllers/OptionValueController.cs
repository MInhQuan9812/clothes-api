using AutoMapper;
using clothes.api.Dtos.Options;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionValueController : BaseController
    {
        private readonly IRepository<OptionValue> _optionValueRepo;
        private readonly IMapper _mapper;



        public OptionValueController(
            IMapper mapper,
            IRepository<OptionValue> optionValueRepo,
            IHttpContextAccessor httpContextAccessor
            ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _optionValueRepo = optionValueRepo;
        }

        [HttpGet("getAllOptionValue")]
        public IActionResult GetAllOptionValue()
        {
            var queryClause = _optionValueRepo.GetQueryableNoTracking()
                                                //.Include(x => x.Option)
                                                .Where(x => !x.IsDeleted);

            var options = new List<OptionValue>();

            options = queryClause.ToList();

            return Ok(_mapper.Map<ICollection<OptionValueDto>>(options));
        }

        [HttpGet("getOptionValueById")]
        public IActionResult GetOptionValueByProductId(int id)
        {
            var option = _optionValueRepo.GetQueryableNoTracking()
                    .Include(x => x.Product)
                                         .Where(x => !x.IsDeleted && x.ProductId == id)
                                         .ToList()
                                         ?? throw new ApplicationException("OptionValue doesn not exist");
            return Ok(_mapper.Map<ICollection<OptionValueDto>>(option));
        }

    }
}
