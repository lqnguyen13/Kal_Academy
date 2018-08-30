using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models.OrderModels
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string BuyerId { get; set; }
        public string StripeToken { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();
        public string PaymentAuthCode { get; set; }
    }

    public enum OrderStatus
    {
        Preparing = 1,
        Shipped = 2,
        Delivered = 3
    }
}
