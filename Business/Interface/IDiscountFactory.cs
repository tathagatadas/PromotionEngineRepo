using PromotionEngine.Business.Models;
using PromotionEngine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Business.Interface
{
    public interface IDiscountFactory
    {
        decimal GetDiscountedPrice(List<ProductSet> ipproducts, DiscountType discounttype);
    }
}
