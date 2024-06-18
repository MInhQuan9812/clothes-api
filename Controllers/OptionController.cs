using AutoMapper;
using clothes.api.Dtos.Category;
using clothes.api.Dtos.Options;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace clothes.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : BaseController
    {
        private readonly IRepository<Option> _optionRepo;
        private readonly IMapper _mapper;

        public OptionController(
            IMapper mapper,
            IRepository<Option> optionRepo,
            IHttpContextAccessor httpContextAccessor
            ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _optionRepo = optionRepo;
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
    }
}
