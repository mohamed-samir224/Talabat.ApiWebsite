using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Config
{
	public class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne( O=>O.ShippingAddress).WithOwner();
			//builder.OwnsOne(O => O.ShippingAddress , ShippingAddress => ShippingAddress.WithOwner());//احمد نصر كاتب كدا بس السطر الي فوق افضل



			//builder.OwnsMany(O => O.OrderItems).WithOwner();


			builder.Property(O => O.OrderStatus)
				.HasConversion(
				OrderStatus => OrderStatus.ToString(),
				OrderStatus => Enum.Parse<OrderStatus>(OrderStatus));
			//OrderStatus =>  (OrderStatus) Enum.Parse(typeof(OrderStatus), OrderStatus); // احمد نصر كاتب كدا بس السطر الي فوق افضل
			



				


		}
	}
}
