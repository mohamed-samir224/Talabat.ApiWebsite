using System.ComponentModel.DataAnnotations;
using Talabat.API.Dtos.AccountDTOS;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.API.Dtos.OrderDTOS
{
	public class CreatedOrderInfosDTO
	{

		
		[Required]
		public string BasketId { get; set; }
		[Required]
		public int DeliveryMethodId	{ get; set; }
		[Required]
		public AddressDto ShippingAddress	{ get; set; }




	}
}
