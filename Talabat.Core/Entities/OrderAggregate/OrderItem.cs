using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
		public OrderItem()
		{

		}
		public OrderItem(ProductInfo product, decimal productPrice, int quantity)
		{
			Product = product;
			ProductPrice = productPrice;
			Quantity = quantity;
		}

		public ProductInfo Product { get; set; }
		public decimal ProductPrice { get; set; }
		public int Quantity { get; set; }

	}
}
