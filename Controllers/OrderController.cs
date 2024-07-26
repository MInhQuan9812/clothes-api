using AutoMapper;
using clothes.api.Common.Seedworks;
using clothes.api.Instrafructure.Entities;
using clothes.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using clothes.api.Dtos.Carts;
using Microsoft.EntityFrameworkCore;

namespace clothes.api.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Order> _orderRepo;

        public OrderController(IMapper mapper,IUnitOfWork unitOfWork,IRepository<Order> orderRepo,IHttpContextAccessor httpContextAccessor): base(httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _orderRepo = orderRepo;
        }

        [HttpGet("getOrderByOrderId")]
        public IActionResult GetOrderByOrderId(int id)
        {

            var order = _orderRepo.GetQueryableNoTracking()
                .Include(x => x.OrderDetails)
                .Include(x => x.Payment)
                .Include(x => x.Promotion)
                .Include(x => x.Customer)
                .Where(x => x.Id == id).ToList() ?? throw new ApplicationException("Khong ton tai order ");

            return Ok(_mapper.Map<ICollection<OrderDto>>(order));
        }

        [HttpGet("GetAllOrderByUserId/{id}")]
        public IActionResult GetAllOrderByUserId(int id)
        {

            var order = _orderRepo.GetQueryableNoTracking()
                .Include(x=>x.OrderDetails)
                .Include(x=>x.Payment)
                .Include(x=>x.Promotion)
                .Include(x=>x.Customer)
                .Where(x => x.CustomerId==id).ToList() ?? throw new ApplicationException("Thanh toan khong thanh cong");

            return Ok(_mapper.Map<ICollection<OrderDto>>(order));
        }

        [HttpPut("updateOrderPaymentStatus")]
        public IActionResult UpdateOrderPaymentStatus([FromQuery] string paymentId, [FromQuery] string token, [FromQuery] string PayerID)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token không có trong URL.");
            }

            string expectedPaymentUrl = $"https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={token}";

            var order = _orderRepo.GetQueryableNoTracking().FirstOrDefault(x => x.PaymentUrl.Equals(expectedPaymentUrl))?? throw new ApplicationException("Thanh toan khong thanh cong");
            
            order.PaymentStatus = true;
            _orderRepo.Update(order.Id, order);
            _orderRepo.SaveChanges();
            return Ok(_mapper.Map<OrderDto>(order));
        }
    }
}
