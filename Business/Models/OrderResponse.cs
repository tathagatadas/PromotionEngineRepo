using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Business.Models
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
    }
}
