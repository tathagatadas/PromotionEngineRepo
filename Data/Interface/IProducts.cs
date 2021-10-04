using PromotionEngine.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Data.Interface
{
    public interface IProducts
    {
        List<Product> GetAllProducts();
        Product GetProductByProductId(int productId);
    }
}
