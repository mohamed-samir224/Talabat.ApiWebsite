using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.Dtos.AccountDTOS;
using Talabat.API.Dtos.OrderDTOS;
using Talabat.API.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories.Contracts;
using Talabat.Core.Services;
using Talabat.Repository.Data;
using Talabat.Service.Services;

namespace Talabat.API.Controllers
{
	[Authorize] // دايما خد بالك ان دا معناه انك كدا كدا معاك اتنين كلاس {User , Claim}
	public class OrdresController : BaseApiController
	{
		private readonly IMapper _mapper;
		private readonly IOrderService _orderService;
		private readonly IUnitofwork _unitofwork;

		public OrdresController( IMapper mapper,IOrderService orderService, IUnitofwork unitofwork)
		{
			_mapper = mapper;
			_orderService = orderService;
			_unitofwork = unitofwork;
		}




		/*  using order service for her method to create order 
		 
				order need some infos like(BasketId,Shippingaddress,DeliveryMethodId,BuyerEmail)

				create Dto with prop carring the data 



		 
		 
		 
		 
		 */

		[HttpPost("CreateOrder")]

		public async Task<ActionResult<Order>> CreateOrder( CreatedOrderInfosDTO OrderInfos) 
		{

			var Buyer = User.FindFirstValue(ClaimTypes.Email);

			var Address = _mapper.Map<AddressDto , AddressShipping>(OrderInfos.ShippingAddress);

			var createdorder = await _orderService.CreateOrderAsync(Buyer, OrderInfos.BasketId, Address , OrderInfos.DeliveryMethodId);
			if(createdorder is not null)
				return Ok(createdorder);
			return BadRequest(new ApiResponse(400));
		
		}


		[HttpGet("GetAllOrders")]
		public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrders() 
		{

			var orders = await _unitofwork.Repository<Order>().GetAllAsync();
			if(orders.Count == 0) return NotFound();
			return Ok(orders);	
		
		
		}
		
	}
}
