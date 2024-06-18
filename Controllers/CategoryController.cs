using AutoMapper;
using clothes.api.Common;
using clothes.api.Dtos.Category;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clothes.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public CategoryController(
            IMapper mapper,
            IRepository<Category> categoryRepo,
            IRepository<Product> productRepo,
            IHttpContextAccessor httpContextAccessor
            ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }


        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var queryClause = _categoryRepo.GetQueryableNoTracking()
                                         .Where(x => !x.IsDeleted);
            var categories = new List<Category>();

            categories = queryClause.ToList();

            return Ok(_mapper.Map<ICollection<CategoryDto>>(categories));
        }

        [HttpPost("createCategory")]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto dto)
        {
            var category = _categoryRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Name.Equals(dto.Name) && !x.IsDeleted);

            if (category != null)
                throw new ApplicationException("Category is already exist");

            category = _categoryRepo.Insert(new Category(){Name=dto.Name,Thumbnail=dto.Thumbnail, IsDeleted=false,CreateTime=DateTime.Now,LastUpdate=DateTime.Now});
            return Ok(_mapper.Map<CategoryDto>(category));  
        }

        [HttpPut("editCategory{id}")]
        //[Authorize(Roles = UserConstants.AdministratorRole)]
        public IActionResult EditCategory(int id, [FromBody] CreateCategoryDto  dto)
        {
            var category = _categoryRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Id==id);
            if (category == null)
                throw new ApplicationException("Category is not exist");
            category.Name=dto.Name;
            category.Thumbnail=dto.Thumbnail;
            category=_categoryRepo.Update(id,category);
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = UserConstants.AdministratorRole)]
        public IActionResult Delete(int id)
        {
            var category = _categoryRepo
              .GetQueryableNoTracking()
              .FirstOrDefault(x => x.Id.Equals(id) && !x.IsDeleted)
               ?? throw new ApplicationException("Category is not exist");

            _categoryRepo.Delete(category);
            return Ok();
        }
    }   
}
