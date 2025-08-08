using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        //----------------------------------------
        //-----Forigen Key------
        public int BrandId { get; set; }

        // ------NA------ Brand  one 2 many 

        public ProductBrand Brand { get; set; }

        //-----Forigen Key------
        public int CategoryId { get; set; }


        // ------NA------ Cate  one 2 many 

        public ProductCategory Category { get; set; }


    }
}
