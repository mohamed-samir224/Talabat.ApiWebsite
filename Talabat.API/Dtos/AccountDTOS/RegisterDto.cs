using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Identity;

namespace Talabat.API.Dtos.AccountDTOS
{
	public class RegisterDto
	{
		[Required]
		public string DisPlayName { get; set; }
		[EmailAddress]
		public string Email { get; set; }


		[Required]
		[RegularExpression("^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*\\d){1,})(?=(.*[^\\da-zA-Z]){1,}).{8,}$",
			ErrorMessage = "Password must be 8+ chars with uppercase, lowercase, and a number.\")")]
		public string Password { get; set; }

		[Required]
		[Phone]
		public string PhoneNumber { get; set; }

		//[RegularExpression(@"^[0-9]{1,3}-[a-zA-z]{5,10}-[a-zA-z]{4,10}-[a-zA-z]{5,10}$"
		//				  , ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
		//public Address UserAddress { get; set; }


	}
}
