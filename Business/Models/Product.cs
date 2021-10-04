using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Business.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public DiscountType DiscountType { get; set; }
    }

    public enum DiscountType
    {
        SingleProduct,
        MultiProduct
    }
}
