using PromotionEngine.Business.Interface;
using PromotionEngine.Business.Models;
using PromotionEngine.Data;
using PromotionEngine.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Business
{
    public class DiscountFactory : IDiscountFactory
    {
        private IDiscount discount;
        public decimal GetDiscountedPrice(List<ProductSet> ipproducts, DiscountType discounttype)
        {
            if (discounttype == DiscountType.SingleProduct)
            {
                discount = new FixedPromotion(new Products());
                return discount.GetDiscountedPrice(ipproducts);
            }
            else
            {
                discount = new CombinePromotion(new Products());
                return discount.GetDiscountedPrice(ipproducts);
            }
        }
    }
}
