using AutoMapper;
using Talabat.API.Dtos;
using Talabat.Core.Entities;

namespace Talabat.API.Helpers
{
	public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _config;
		public ProductUrlResolver(IConfiguration config)
		{
			_config = config;
		}
		public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return _config["ApiUrl"] + "Images/" +source.PictureUrl;
			}
			return null;
		}
	}
	
	
}