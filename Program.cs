using PromotionEngine.Business;
using PromotionEngine.Business.Models;
using PromotionEngine.Data;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Scenario A
            1 * A 50
            1 * B 30
            1 * C 20
            */

            OrderBusinessLogic orderBusinessLogic = new OrderBusinessLogic(new DiscountFactory(), new Products());

            OrderRequest orderReqest1 = new OrderRequest();
            orderReqest1.OrderId = 1;
            orderReqest1.OrderByProducts = new List<OrderByProduct>();
            orderReqest1.OrderByProducts.Add(new OrderByProduct 
            {
                ProductId = 1,
                Quantity = 1
            });
            orderReqest1.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 2,
                Quantity = 1
            });
            orderReqest1.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 3,
                Quantity = 1
            });
            decimal billamount1 = orderBusinessLogic.CalcuateBillAmont(orderReqest1);
            Console.WriteLine("Scenario A total amount " + billamount1.ToString());

            /*
            Scenario B
            5 * A 50
            5 * B 30
            1 * C 20
            */

            OrderRequest orderReqest2 = new OrderRequest();
            orderReqest2.OrderId = 2;
            orderReqest2.OrderByProducts = new List<OrderByProduct>();
            orderReqest2.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 1,
                Quantity = 5
            });
            orderReqest2.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 2,
                Quantity = 5
            });
            orderReqest2.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 3,
                Quantity = 1
            });
            decimal billamount2 = orderBusinessLogic.CalcuateBillAmont(orderReqest2);
            Console.WriteLine("Scenario B total amount " + billamount2.ToString());

            /*
            Scenario C
            3 * A 50
            5 * B 30
            1 * C 20
            1 * D 15
            */

            OrderRequest orderReqest3 = new OrderRequest();
            orderReqest3.OrderId = 3;
            orderReqest3.OrderByProducts = new List<OrderByProduct>();
            orderReqest3.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 1,
                Quantity = 3
            });
            orderReqest3.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 2,
                Quantity = 5
            });
            orderReqest3.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 3,
                Quantity = 1
            });
            orderReqest3.OrderByProducts.Add(new OrderByProduct
            {
                ProductId = 4,
                Quantity = 1
            });
            decimal billamount3 = orderBusinessLogic.CalcuateBillAmont(orderReqest3);
            Console.WriteLine("Scenario C total amount " + billamount3.ToString());

            Console.ReadKey();
        }
    }
}
