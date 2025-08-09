using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class ProductInfo
    {
		public ProductInfo()
		{
				
		}
		public ProductInfo(int prodId ,string prodName , string ProdUrl)
		{
			ProductId = prodId;
			ProductName = prodName;
			ProductUrl = ProdUrl;
		}
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductUrl { get; set; }
	}
}
