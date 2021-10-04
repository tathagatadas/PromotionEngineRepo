using PromotionEngine.Business.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PromotionEngine.Data.Interface;

namespace PromotionEngine.Data
{
    public class FixedPromotion : IDiscount
    {
        private readonly Dictionary<int, FixedRate> FixedRateDetails;
        private readonly IProducts products;
        public FixedPromotion(IProducts _products)
        {
            products = _products;

            //Load details from Configuration
            FixedRateDetails = new Dictionary<int, FixedRate>();
            FixedRateDetails.Add(1, new FixedRate { Quantity = 3, DiscountedPrice = 130 });
            FixedRateDetails.Add(2, new FixedRate { Quantity = 2, DiscountedPrice = 45 });
        }
        public decimal GetDiscountedPrice(List<ProductSet> ipproducts)
        {
            int round = 0;
            decimal response = 0;

            var input = ipproducts.FirstOrDefault();

            var item = products.GetProductByProductId(input.ProductId);

            var availableDiscount = FixedRateDetails[input.ProductId];

            
            round = (int)input.Quantity / availableDiscount.Quantity;

            if (round == 0)
                response = input.Quantity * item.Price;
            else if (input.Quantity % availableDiscount.Quantity == 0)
                response = (round * availableDiscount.DiscountedPrice);
            else
                response = ((round * availableDiscount.DiscountedPrice) + ((input.Quantity % availableDiscount.Quantity) 
                    * item.Price));            

            return response;
        }
    }

    public class FixedRate
    {
        public int Quantity { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
