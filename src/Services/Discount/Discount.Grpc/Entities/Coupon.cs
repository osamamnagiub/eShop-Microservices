﻿namespace Discount.Grpc.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public string  ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public static readonly Coupon NoDisCount = new()
            { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
    }
}
