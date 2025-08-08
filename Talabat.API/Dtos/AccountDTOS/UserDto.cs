using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos.AccountDTOS
{
	public class UserDto
	{
		public string DisPlayName { get; set; }

		[DataType(DataType.EmailAddress)]

		public string Email { get; set; }

		public string Token { get; set; }
	}
}
