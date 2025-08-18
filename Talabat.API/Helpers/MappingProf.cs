using AutoMapper;
using Talabat.API.Dtos;
using Talabat.API.Dtos.AccountDTOS;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.API.Helpers
{
	public class MappingProf : Profile
	{

		public MappingProf()
		{
			CreateMap<Product, ProductToReturnDto>().ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand.Name))
				                                    .ForMember(d => d.Category, O => O.MapFrom(s => s.Category.Name))
													.ForMember(d => d.PictureUrl, O => O.MapFrom<ProductUrlResolver>());



			CreateMap<Address, AddressDto>().ReverseMap().ReverseMap();

			CreateMap<CustomerBasketDto, CustomerBasket>();

			CreateMap<AddressDto, AddressShipping>().ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.FirstName))
												 .ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.LastName));
												 
		}
	}
}
