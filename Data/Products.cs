using PromotionEngine.Business.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PromotionEngine.Data.Interface;

namespace PromotionEngine.Data
{
    public class Products : IProducts
    {
        //Load from Database
        public List<Product> GetAllProducts()
        {
            List<Product> response = new List<Product>();

            response.Add(new Product { ProductId = 1, ProductName = "A", Price = 50, DiscountType = DiscountType.SingleProduct });
            response.Add(new Product { ProductId = 2, ProductName = "B", Price = 30, DiscountType = DiscountType.SingleProduct });
            response.Add(new Product { ProductId = 3, ProductName = "C", Price = 20, DiscountType = DiscountType.MultiProduct });
            response.Add(new Product { ProductId = 4, ProductName = "D", Price = 15, DiscountType = DiscountType.MultiProduct });
            response.Add(new Product { ProductId = 5, ProductName = "E", Price = 10, DiscountType = DiscountType.MultiProduct });
            response.Add(new Product { ProductId = 6, ProductName = "F", Price = 9, DiscountType = DiscountType.MultiProduct });

            return response;
        }
        public Product GetProductByProductId(int productId)
        {
            List<Product> products = GetAllProducts();

            return products.Where(x => x.ProductId == productId).FirstOrDefault();
        }
    }
}
