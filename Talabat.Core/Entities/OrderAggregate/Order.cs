using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
		public Order()
		{
			
		}
		public Order(string buyerEmail, AddressShipping shippingAddress, ICollection<OrderItem> orderItem, DeliveryMethod deliveryMethod, decimal subtotal)
		{
			BuyerEmail = buyerEmail;
			ShippingAddress = shippingAddress;
			OrderItems = orderItem;
			DeliveryMethod = deliveryMethod;
			SubTotal = subtotal;
		}

		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;


		//public string AddressShippingId { get; set; } // Foreign key to the AddressShipping entity
		public AddressShipping? ShippingAddress { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
		public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
		public decimal SubTotal { get; set; }
		public decimal GetTotal() 
		{
			SubTotal += DeliveryMethod.Cost;
			return SubTotal;

		}
		public string PaymentIntentId { get; set; } = string.Empty;

	}
}
