using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.Services.abstractions;
using Stripe;
using Stripe.Checkout;

namespace Project.BL.Services.implemantantions
{
    public class StripeService : IStripeService
    {
        private readonly string _secretKey = "sk_test_51Qrh4401Fe4d1keHXViBGZ8qZjnBsqw1tfnG1B34BKwjy3OQIA0HmsBwJzFTnC1VUK2MKC3VZA6xNfQcg2t6avTG00w9Jl7r8o";

        public StripeService()
        {
            StripeConfiguration.ApiKey = _secretKey;
        }

        public async Task<string> CreateCheckoutSession(double amount, string currency)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
         {
             new SessionLineItemOptions
             {
                 PriceData = new SessionLineItemPriceDataOptions
                 {
                     Currency = currency,
                     UnitAmount = (long)(amount * 100),
                     ProductData = new SessionLineItemPriceDataProductDataOptions
                     {
                         Name = "Order Payment"
                     }
                 },
                 Quantity = 1
             }
         },
                Mode = "payment",
                SuccessUrl = "https://localhost:7290/Cart/PaymentSuccess",
                CancelUrl = "https://localhost:7290/Cart/PaymentFailed",
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session.Url; // Redirect üçün URL qaytarır

        }
    }
}
