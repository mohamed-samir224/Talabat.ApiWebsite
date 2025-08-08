using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
	public class CustomerBasketDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		[Range(0.1 , double.MaxValue)]
		public decimal Price { get; set; }
		[Required]
		[Range(1,int.MaxValue , ErrorMessage= "Should be any Item" )]
		public int Quantity { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Type { get; set; }
	}
}
