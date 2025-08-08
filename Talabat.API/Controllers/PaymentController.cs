using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Talabat.Core.Entities;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class PaymentController : ControllerBase
	{
		private readonly CustomerBasket cb;

		public PaymentController(CustomerBasket cb)
		{
			this.cb = cb;
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent() 
		{

		
			CustomerBasket cbb = new CustomerBasket("20" ,new List<BasketItem> { });

			return cbb;
		
		
		}
	}
}
