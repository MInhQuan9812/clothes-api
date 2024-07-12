using clothes.api.Instrafructure.Services.Paypal;

namespace clothes.api.Instrafructure.DesignPattern.Facade
{
    public class ServiceFacade
    {
        public static ServiceFacade _instance;
        private PaypalService _paypalService;
        //private EmailService _emailService;

        private ServiceFacade(IConfiguration configuration)
        {
            _paypalService = new PaypalService(configuration);
        }

        public static ServiceFacade Instance(IConfiguration configuration)
        {

            if (_instance == null)
            {
                _instance = new ServiceFacade(configuration);
            }
            return _instance;
        }

        public async Task<string> PaymentWithPaypall(int amount)
        {
            int _amount = amount / 25000;
            string returnUrl = "http://localhost:5117/api/Order/UpdateOrderPaymentStatus";
            string cancelUrl = "https://localhost:5117/error/";

            var createdPayment = await _paypalService.CreateOrderAsync(_amount, returnUrl, cancelUrl);

            string approvalUrl = createdPayment.links.FirstOrDefault(x => x.rel.ToLower() == "approval_url")?.href;

            return await Task.FromResult(approvalUrl);
        }

        public async Task RecoveryPasswordWithEmail()
        {

        }
    }
}
