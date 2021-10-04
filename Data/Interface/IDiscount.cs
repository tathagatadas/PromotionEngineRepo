using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Data.Interface
{
    public interface IDiscount
    {
        decimal GetDiscountedPrice(List<ProductSet> ipproducts);
    }
}
