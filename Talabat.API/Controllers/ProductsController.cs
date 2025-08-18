using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contracts;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;

namespace Talabat.API.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenaricRepository<Product> _genaricRepository;
		private readonly AutoMapper.IMapper _mapper;

		public ProductsController(IGenaricRepository<Product> genaricRepository , IMapper mapper )
		{
			_genaricRepository = genaricRepository;
			_mapper = mapper;
		}
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAll([FromQuery] ProductParams productParams /*string? sort , int? brandId, int? CategoryId*/)
		{

			var spec = new ProductWithBrandAndCategorySpecifications(productParams);
	
			var Products = await _genaricRepository.GetAllWithSpecAsync(spec);

			var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(Products);

			var specforCount = new CountAfterFilterationNdSortingWithoutPaginationForProduct(productParams);

			var Count = await _genaricRepository.GetCountWithSpecAsync(specforCount);


			return Ok(new Pagination<ProductToReturnDto>(productParams.PageSize , productParams.PageIndex, Count, data));
		}


		[HttpGet("{Id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetById(int Id) 
		{
			var spec = new ProductWithBrandAndCategorySpecifications(Id);

			var product =await _genaricRepository.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(new ApiResponse(404));


			return Ok(_mapper.Map<Product , ProductToReturnDto>(product));
		
		}
	
	
	
	}
}

