using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductParams
    {

        public string? sort { get; set; }

		public int? brandId { get; set; }
		public int? categoryId { get; set; }
		public int PageIndex { get; set; } 
		public int PageSize { get; set; }

		private string? search;

		public string? Search
		{
			get { return search; }
			set { search = value?.ToLower(); }
		}

	}
}
