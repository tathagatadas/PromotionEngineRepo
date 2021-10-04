using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Business.Models
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        public List<OrderByProduct> OrderByProducts { get; set; }
    }
}
