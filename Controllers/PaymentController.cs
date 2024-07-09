using AutoMapper;
using clothes.api.Common.Seedworks;
using clothes.api.Dtos.Carts;
using clothes.api.Dtos.Payments;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace clothes.api.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Payment> _paymentRepo;

        public PaymentController
            (
                IMapper mapper,
                IUnitOfWork unitOfWork,
                IRepository<Payment> paymentRepo,
                IHttpContextAccessor httpContextAccessor
                ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _paymentRepo = paymentRepo;
        }


        [HttpGet("getAllPaymentMethod")]
        public IActionResult GetAllPaymentMethod()
        {
            var payment = _paymentRepo.GetQueryableNoTracking()
                                .Where(x => !x.IsDeleted);
            return Ok(_mapper.Map<ICollection<Payment>>(payment));
        }


        [HttpPost("createPaymentMethod")]
        public IActionResult CreatePaymentMethod([FromBody] CreatePaymentDto dto)
        {
            var payment = _paymentRepo
                .GetQueryableNoTracking()
                .Any(x => x.Title.Equals(dto.Title));

            if (payment)
            {
                throw new ApplicationException("Payment is already exists");
            }

            Payment newPayment = new Payment
            {
                Title = dto.Title,
            };
            
            _paymentRepo.Insert(newPayment);
            _paymentRepo.SaveChanges();

            return Ok(_mapper.Map<Payment>(newPayment));
        }


    }
}
