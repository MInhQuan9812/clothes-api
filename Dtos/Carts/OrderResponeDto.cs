using clothes.api.Dtos.User;
using clothes.api.Instrafructure.Entities;
using PayPal.Api;

namespace clothes.api.Dtos.Carts
{
    public class OrderResponeDto
    {
        public string Url { get; set; }
        public OrderDto Order { get; set; }

        public OrderResponeDto(string url,OrderDto order)
        {
            Url = url;
            Order = order;
        }
    }
}
