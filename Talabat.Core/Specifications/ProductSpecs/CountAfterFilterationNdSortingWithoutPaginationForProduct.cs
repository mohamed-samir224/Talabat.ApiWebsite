using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class CountAfterFilterationNdSortingWithoutPaginationForProduct : BaseSpecifications<Product>
    {
		public CountAfterFilterationNdSortingWithoutPaginationForProduct(ProductParams productParams)
			:base( P => (string.IsNullOrEmpty(productParams.Search) || P.Name.Contains(productParams.Search))&&
			        (!productParams.brandId.HasValue || P.BrandId == productParams.brandId.Value)&&  // كدا كدا الجزء التاني من ال ||, هيديني ترو ف هيطلع ترو ف مش هزعل اني عامل   (!) قدام عكستلي الحقيق)
					(!productParams.categoryId.HasValue || P.CategoryId == productParams.categoryId.Value))
		{

			/// ------->>>>>>
			/// كنت عايز اعمل sorting هنا بس لقيت مالهاش لازمه هي مجرد حساب لعدد مش محتاج ترتيب خالص

			//if (!string.IsNullOrEmpty(productParams.sort))
			//{
			//	switch (productParams.sort)
			//	{

			//		case ("Price")
			//			:
			//			OrderBy = P => P.Price;
			//			break;
			//		case ("PriceDesc")
			//			:
			//			OrderByDesc = P => P.Price;
			//			break;
			//		default:
			//			OrderBy = P => P.Name; break;
			//	}
			//}
			//else
			//{
			//	OrderBy = P => P.Name;
			//}


		}
		
		
			
		
	}
}
