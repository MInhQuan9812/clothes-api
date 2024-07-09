using AutoMapper;
using clothes.api.Dtos.Category;
using clothes.api.Dtos.Options;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : BaseController
    {
        private readonly IRepository<Option> _optionRepo;
        private readonly IRepository<OptionValue> _optionValueRepo;
        private readonly IMapper _mapper;

        public OptionController(
            IMapper mapper,
            IRepository<Option> optionRepo,
            IRepository<OptionValue> optionValueRepo,
            IHttpContextAccessor httpContextAccessor
            ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _optionRepo = optionRepo;
            _optionValueRepo = optionValueRepo;
        }

        [HttpGet]
        public IActionResult GetAllOption()
        {
            var queryClause = _optionRepo.GetQueryableNoTracking()

                                         .Where(x => !x.IsDeleted);
            var options = new List<Option>();

            options = queryClause.ToList();

            return Ok(_mapper.Map<ICollection<OptionDto>>(options));
        }

        //[HttpGet]
        //public IActionResult GetOptionToOptionValueToFindVariantByOptionId(int id)
        //{
        //    var queryClause = _optionRepo.GetQueryableNoTracking()
        //                                    .Include(x=>x.OptionValues)
        //                                    .ThenInclude(x=>x.VarientValues)
        //                                 .Where(x => !x.IsDeleted);
        //    var options = new List<Option>();

        //    options = queryClause.ToList();

        //    return Ok(_mapper.Map<ICollection<OptionDto>>(options));
        //}




        [HttpPost("createOption")]
        public IActionResult CreateCategory([FromBody] CreateOptionDto dto)
        {
            var category = _optionRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Name.Equals(dto.Name) && !x.IsDeleted);

            if (category != null)
                throw new ApplicationException("Option is already exist");

            category = _optionRepo.Insert(new Option() { Name = dto.Name, IsDeleted = false, CreateTime = DateTime.Now, LastUpdate = DateTime.Now });
            return Ok(_mapper.Map<OptionDto>(category));
        }

        [HttpPut("editOption{id}")]
        //[Authorize(Roles = UserConstants.AdministratorRole)]
        public IActionResult EditCategory(int id, [FromBody] CreateOptionDto dto)
        {
            var option = _optionRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Id == id);
            if (option == null)
                throw new ApplicationException("Option is not exist");
            option.Name = dto.Name;
            option = _optionRepo.Update(id, option);
            return Ok(_mapper.Map<CreateOptionDto>(option));
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = UserConstants.AdministratorRole)]
        public IActionResult Delete(int id)
        {
            var category = _optionRepo
              .GetQueryableNoTracking()
              .FirstOrDefault(x => x.Id.Equals(id) && !x.IsDeleted)
               ?? throw new ApplicationException("Option is not exist");

            _optionRepo.Delete(category);
            return Ok();
        }

        [HttpPost("addValueToOption/{id}")]
        public IActionResult AddValueToOption(int id, [FromBody] AddValueToOptionDto dto)
        {
            var option = _optionRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Id.Equals(id) && !x.IsDeleted)
                ?? throw new ApplicationException("Option is not exist");

            if (_optionValueRepo.GetQueryableNoTracking().FirstOrDefault
                (
                    x => x.OptionId == option.Id && x.Value == dto.Value && !x.IsDeleted) != null
                )
                throw new ApplicationException("This option's value is already exits in option");

            var optionValue = _optionValueRepo.Insert(new OptionValue() { OptionId = option.Id, Value = dto.Value });
            return Ok(_mapper.Map<AddValueToOptionDto>(optionValue));

        }
    }
}
