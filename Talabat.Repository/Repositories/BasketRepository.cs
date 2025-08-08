using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contracts;

namespace Talabat.Repository.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;
		public BasketRepository(IConnectionMultiplexer Redis)
		{
			_database = Redis.GetDatabase();
		}
		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}

		public async Task<CustomerBasket?> GetBasketAsync(string basketId)
		{


			var Basket = await _database.StringGetAsync(basketId);
			if (Basket.IsNull)
				return null;

			var BasketString = Basket.ToString();
			if(string.IsNullOrEmpty(BasketString))                                 //// Just Handling Warning
				return null;

			else 
				return JsonSerializer.Deserialize<CustomerBasket>(BasketString);



		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
		{
			var CreateOrUpdate = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));

			if(CreateOrUpdate)
			return await GetBasketAsync(basket.Id);
			else 
				return null;

			
				
				
				
				
				}
	}
}
