using PromotionEngine.Business.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PromotionEngine.Data.Interface;

namespace PromotionEngine.Data
{
    public class CombinePromotion : IDiscount
    {
        private readonly List<CombineRate> CombineRateDetails;
        private readonly IProducts products;
        public CombinePromotion(IProducts _products)
        {
            products = _products;

            //Load details from Configuration
            CombineRateDetails = new List<CombineRate>();

            //Assumed there is no mixed combination
            var combo1 = new List<ProductSet>();
            combo1.Add(new ProductSet { ProductId = 3, Quantity = 1 });
            combo1.Add(new ProductSet { ProductId = 4, Quantity = 1 });

            var combo2 = new List<ProductSet>();
            combo2.Add(new ProductSet { ProductId = 5, Quantity = 2 });
            combo2.Add(new ProductSet { ProductId = 6, Quantity = 1 });

            CombineRateDetails.Add(new CombineRate { CombineSets = combo1, DiscountedPrice = 30 });
            CombineRateDetails.Add(new CombineRate { CombineSets = combo2, DiscountedPrice = 25 });

        }
        public decimal GetDiscountedPrice(List<ProductSet> ipproducts)
        {
            decimal response = 0;
            List<RemainigItem> remainigItems = new List<RemainigItem>();

            if (ipproducts.Count == 1)
            {
                response = GetSignleProductPrice(ipproducts.FirstOrDefault());
            }

            else
            {
                var combo = CombineRateDetails.Where(x => x.CombineSets.Any(x => x.ProductId
                                == ipproducts.FirstOrDefault().ProductId)).FirstOrDefault();

                int prod1quantity = ipproducts.First().Quantity;
                int prod2quantity = ipproducts.Last().Quantity;

                if((prod1quantity >= combo.CombineSets.Where(x => x.ProductId == ipproducts.First().ProductId).FirstOrDefault().Quantity
                        && prod2quantity >= combo.CombineSets.Where(x => x.ProductId == ipproducts.Last().ProductId).FirstOrDefault().Quantity))
                {
                    int prod1remquantity = prod1quantity % combo.CombineSets.Where(x => x.ProductId == ipproducts.First().ProductId).FirstOrDefault().Quantity;
                    if(prod1remquantity != 0)
                    {
                        response = response + GetSignleProductPrice(
                            new ProductSet 
                            { 
                                ProductId = ipproducts.First().ProductId,
                                Quantity = prod1remquantity
                            });
                    }

                    int prod2remquantity = prod2quantity % combo.CombineSets.Where(x => x.ProductId == ipproducts.First().ProductId).FirstOrDefault().Quantity;
                    if (prod2remquantity != 0)
                    {
                        response = response + GetSignleProductPrice(
                            new ProductSet
                            {
                                ProductId = ipproducts.Last().ProductId,
                                Quantity = prod1remquantity
                            });
                    }

                    var comcount = ipproducts.First().Quantity / combo.CombineSets.Where(x => x.ProductId == ipproducts.First().ProductId).FirstOrDefault().Quantity;
                    response = response + combo.DiscountedPrice * comcount;
                }
                else
                {
                    foreach(var item in ipproducts)
                    {
                        response = response + GetSignleProductPrice(item);
                    }
                }

            }
            return response;
        }

        private decimal GetSignleProductPrice(ProductSet ipproduct)
        {
            var item = products.GetProductByProductId(ipproduct.ProductId);
            return ipproduct.Quantity * item.Price;
        }
    }


    public class CombineRate
    {
        public List<ProductSet> CombineSets { get; set; }
        public decimal DiscountedPrice { get; set; }
    }

    public class RemainigItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
