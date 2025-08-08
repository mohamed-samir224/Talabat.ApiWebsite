using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contracts;

namespace Talabat.API.Controllers
{
    public class BasketController : BaseApiController
    {
		private readonly  IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository , IMapper mapper)
		{
			_basketRepository = basketRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id) //// GET   Or ReCreate After Expire
		{
			if (id is not null)
			{
				var Basket = await _basketRepository.GetBasketAsync(id);
				if (Basket == null) 
				{
				return NotFound(new ApiResponse(404));
				
				}
				return new CustomerBasket(id ,Basket.Items);
			}
				return BadRequest(new ApiResponse(400));
			
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> CreateBasket(CustomerBasketDto customerBasket) 
		{
			var MappedCustomerBasket =  _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);

			if (customerBasket is not null) 
			{
				var CustBasket = await _basketRepository.UpdateBasketAsync(MappedCustomerBasket);
				if(CustBasket is not null)
					return Ok(CustBasket);
				else
					return BadRequest(new ApiResponse(400));
			}
					return BadRequest(new ApiResponse(400));


		}
		[HttpDelete]
		public async Task<ActionResult<JsonResult>> DeleteBasket(CustomerBasket customerBasket)
		{

			if (customerBasket is not null)
			{
				var res = await _basketRepository.DeleteBasketAsync(customerBasket.Id);
				if (res) return Ok();
				else return BadRequest(new ApiResponse(400, "In "));
			}

			return BadRequest(new ApiResponse(400, "Out"));
		}






	}
}
