using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories.Contracts;
using Talabat.Core.Services;

namespace Talabat.Service.Services
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IGenaricRepository<Product> _productrepo;
		private readonly IGenaricRepository<DeliveryMethod> _deliveryrepo;

		public OrderService(IBasketRepository basketRepository , IGenaricRepository<Product> Productrepo , IGenaricRepository<DeliveryMethod> deliveryrepo)
		{
			_basketRepository = basketRepository;
			_productrepo = Productrepo;
			_deliveryrepo = deliveryrepo;
		}
		public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, AddressShipping Shippingaddress, int DeliveryMethodId)
		{
			var Basket = await _basketRepository.GetBasketAsync(BasketId);
			//-----------------------------------------------------------------------------------------

			var ItemsForOrders = new List<OrderItem>();

			//var itemsinBasket = await _basketRepository.GetBasketAsync(BasketId);

			if (Basket.Items?.Count() > 0) 
			{
				foreach (var item in Basket.Items) 
				{
				    var product = await _productrepo.GetByIdAsync(item.Id);

					var productinfo = new ProductInfo(item.Id , product.Name ,product.PictureUrl);

					var ItemsForOrder = new OrderItem(productinfo, item.Price, item.Quantity);
				}
			}
			//-------------------------------------------------------------------------------------------------
			//انت عايز تمسك ف ايدك طريقه التوصيل الصحيحه للاورد ايل يخي موجوده في جدول تاني برضه ف عايز تعملهاا جت ف امسك الجينايرك ريبو وبعدين اديله الديليفري ميثود ك تي واستخدم الميثود بتاعته الي بتجيب الحاجه الي انت عايزها عموما بقا من الداتا بيز


			var SubTotal = ItemsForOrders.Sum(x => x.Quantity * x.ProductPrice);

			var deliverymethod = await _deliveryrepo.GetByIdAsync(DeliveryMethodId);

			var order = new Order(BuyerEmail, Shippingaddress, ItemsForOrders, deliverymethod, SubTotal);

			return order;
			


		}




	}
}
