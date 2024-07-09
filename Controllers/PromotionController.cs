using AutoMapper;
using clothes.api.Common.Seedworks;
using clothes.api.Dtos.Category;
using clothes.api.Dtos.Promotions;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Controllers
{
    [Route("api/Promotion")]
    [ApiController]
    public class PromotionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Promotion> _promotionRepo;
        private readonly IRepository<PromotionType> _promotionTypeRepo;

        public PromotionController(IMapper mapper,IUnitOfWork unitOfWork,IRepository<Promotion> promotionRepo,IRepository<PromotionType> promotionTypeRepo,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _mapper= mapper;
            _unitOfWork= unitOfWork;
            _promotionRepo= promotionRepo;
            _promotionTypeRepo= promotionTypeRepo;
        }

        [HttpGet("getAllPromotion")]
        public IActionResult GetAllPromotion()
        {
            var promotion = _promotionRepo.GetQueryableNoTracking()
                .Include(x=>x.PromotionType)
                .Where(x => !x.IsDeleted);
            return Ok(_mapper.Map<ICollection<PromotionDto>>(promotion));
        }

        [HttpGet("getAllPromotionType")]
        public IActionResult GetAllPromotionType()
        {
            var promotionType = _promotionTypeRepo.GetQueryableNoTracking()
                .Where(x => !x.IsDeleted);
            return Ok(_mapper.Map<ICollection<PromotionTypeDto>>(promotionType));
        }

        [HttpPost("createPromotionType")]
        public IActionResult CreatePromotionType([FromBody] PromotionTypeDto dto)
        {
            var promotionType = _promotionTypeRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Title.Equals(dto.Title) && !x.IsDeleted);

            if (promotionType != null)
                throw new ApplicationException("Promotion type is already exist");

            promotionType = _promotionTypeRepo.Insert(new PromotionType() { Title = dto.Title, IsDeleted = false, CreateTime = DateTime.Now, LastUpdate = DateTime.Now });
            return Ok(_mapper.Map<PromotionTypeDto>(promotionType));
        }


        [HttpPost("createPromotion")]
        public IActionResult CreatePromotion([FromBody] PromotionDto dto)
        {
            var promotion = _promotionRepo
                .GetQueryableNoTracking()
                .FirstOrDefault(x => x.Name.Equals(dto.Name) && !x.IsDeleted);

            if (promotion != null)
                throw new ApplicationException("Category is already exist");

            promotion = _promotionRepo.Insert(new Promotion() { Name = dto.Name, PromotionValue=dto.PromotionValue,PromotionTypeId=dto.PromotionTypeId, IsDeleted = false, CreateTime = DateTime.Now, LastUpdate = DateTime.Now });
            return Ok(_mapper.Map<PromotionDto>(promotion));
        }
    }
}
