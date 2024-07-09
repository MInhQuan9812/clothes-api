using PayPal.Api;

namespace clothes.api.Instrafructure.Services.Paypal
{
    public class PaypalService : IPaypallService
    {
        private readonly APIContext _apiContext;
        private readonly Payment _payment;
        private readonly IConfiguration _configuration;

        public PaypalService(IConfiguration configuration)
        {
            _configuration = configuration;

            var clientId = _configuration["PayPal:ClientId"];
            var clientSecret = _configuration["PayPal:ClientSecret"];

            var config = new Dictionary<string, string>
            {
                {"mode","sandbox" },
                {"clientId",clientId },
                {"clientSecret",clientSecret }
            };

            var accessToken = new OAuthTokenCredential(clientId, clientSecret, config).GetAccessToken();
            _apiContext = new APIContext(accessToken);

            _payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" }
            };
        }

        public async Task<Payment> CreateOrderAsync(int amount, string returnUrl, string cancleUrl)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(_configuration["PayPal:ClientId"], _configuration["PayPal:ClientSecret"]).GetAccessToken());
            var itemList = new ItemList()
            {
                items = new List<Item>()
                {
                    new Item()
                    {
                        name="Membership Fee",
                        currency="USD",
                        price=amount.ToString("0.00"),
                        quantity="1",
                        sku="membership"
                    }
                }
            };

            var transaction = new Transaction()
            {
                amount = new Amount()
                {
                    currency = "USD",
                    total = amount.ToString("0.00"),
                    details = new Details()
                    {
                        subtotal = amount.ToString("0.00")
                    }
                },
                item_list = itemList,
                description = "Membership Fee"
            };

            var payment = new Payment()
            {
                intent = "sale",
                payer = new Payer() { payment_method = "paypal" },
                redirect_urls = new RedirectUrls()
                {
                    return_url = returnUrl,
                    cancel_url = cancleUrl,
                },
                transactions = new List<Transaction>() { transaction }
            };

            var createdPayment = payment.Create(apiContext);
            return createdPayment;
        }
    }
}