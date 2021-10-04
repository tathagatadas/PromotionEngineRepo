using PromotionEngine.Business.Interface;
using PromotionEngine.Business.Models;
using PromotionEngine.Data.Interface;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PromotionEngine.Data;

namespace PromotionEngine.Business
{
    public class OrderBusinessLogic
    {
        private readonly IDiscountFactory discountcalc;
        private readonly IProducts products;
        private readonly List<MultiProuctDiscountConfig> multiProuctDiscountConfigs;
        private int ignoredid = 0;

        public OrderBusinessLogic(IDiscountFactory _discountcalc, IProducts _products)
        {
            discountcalc = _discountcalc;
            products = _products;

            multiProuctDiscountConfigs = new List<MultiProuctDiscountConfig>();

            multiProuctDiscountConfigs.Add(new MultiProuctDiscountConfig 
            {
                FirstProductId = 3,
                SecondProductId = 4
            });
            multiProuctDiscountConfigs.Add(new MultiProuctDiscountConfig
            {
                FirstProductId = 5,
                SecondProductId = 6
            });
        }

        public decimal CalcuateBillAmont(OrderRequest orderReqest)
        {
            decimal response = 0;
            var allproducts = products.GetAllProducts();
            List<ProductSet> ipproducts;

            foreach (var order in orderReqest.OrderByProducts)
            {
                if (allproducts.FirstOrDefault(x => x.ProductId == order.ProductId).DiscountType == DiscountType.SingleProduct)
                {
                    ipproducts = new List<ProductSet>();
                    ipproducts.Add(new ProductSet
                    {
                        ProductId = order.ProductId,
                        Quantity = order.Quantity
                    });
                    response = response + discountcalc.GetDiscountedPrice(ipproducts, DiscountType.SingleProduct);
                }
                else
                {
                    if(multiProuctDiscountConfigs.Any(x => x.FirstProductId == order.ProductId || x.SecondProductId == order.ProductId))
                    {
                        if (order.ProductId == ignoredid)
                            continue;

                        ipproducts = new List<ProductSet>();
                        ipproducts.Add(new ProductSet
                        {
                            ProductId = order.ProductId,
                            Quantity = order.Quantity
                        });

                        var com = multiProuctDiscountConfigs.Where(x => x.FirstProductId == order.ProductId || x.SecondProductId == order.ProductId).FirstOrDefault();

                        int otherproductId = com.FirstProductId == ipproducts.FirstOrDefault().ProductId ? com.SecondProductId : com.FirstProductId;

                        if(orderReqest.OrderByProducts.Any( x => x.ProductId == otherproductId))
                        {
                            ipproducts.Add(new ProductSet
                            {
                                ProductId = otherproductId,
                                Quantity = orderReqest.OrderByProducts.FirstOrDefault(x => x.ProductId == otherproductId).Quantity
                            });
                            ignoredid = otherproductId;
                        }

                        response = response + discountcalc.GetDiscountedPrice(ipproducts, DiscountType.MultiProduct);
                    }

                }
            }

            return response;
        }
    }

    public class MultiProuctDiscountConfig
    {
        public int FirstProductId { get; set; }
        public int SecondProductId { get; set; }

    }
}
