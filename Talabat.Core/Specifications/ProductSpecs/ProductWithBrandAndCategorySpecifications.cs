using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
  //      public ProductWithBrandAndCategorySpecifications(/*int? id*/) 
		//	:base()
  //      {
		//	///if (id is not null)
		//	///{
		//	///     Expression<Func<Product , bool>> crit =criteria = (P => P.id == id);
		//	///}
		//	///if(criteria is not null)
		//	///criteria.Compile();
		//	///includes.Add(P => P.Brand);
		//	///includes.Add(P => P.Category);

		//	includes.Add(P => P.Brand);

		//	includes.Add(P => P.Category);
		//}
        public ProductWithBrandAndCategorySpecifications(int? id) : base(P => P.id == id) 
        {
			includes.Add(P => P.Brand);

			includes.Add(P => P.Category);

		}

		public ProductWithBrandAndCategorySpecifications(ProductParams productParams)
			:base( P => (string.IsNullOrEmpty(productParams.Search) || P.Name.Contains(productParams.Search) )&&
						(!productParams.brandId.HasValue || P.BrandId == productParams.brandId.Value ) &&  // كدا كدا الجزء التاني من ال ||  هيديني ترو ف هيطلع ترو ف مش هزعل اني عامل   (!) قدام عكستلي الحقيقه
						(!productParams.categoryId.HasValue || P.CategoryId == productParams.categoryId.Value ) )


		{
			includes.Add(P => P.Brand);

			includes.Add(P => P.Category);

			if (!string.IsNullOrEmpty(productParams.sort))
			{
				switch (productParams.sort)
				{

					case ("Price")
						:
						OrderBy = P => P.Price;
						break;
					case ("PriceDesc")
						:
						OrderByDesc = P => P.Price;
						break;
					default:
						OrderBy = P => P.Name; break;
				}
			}
			else
			{
				OrderBy = P => P.Name;}


				ApplayPagination((productParams.PageIndex-1) * productParams.PageSize, productParams.PageSize);

			
		}

	}
}
