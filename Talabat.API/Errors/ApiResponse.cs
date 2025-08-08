
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;

namespace Talabat.API.Errors
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }

		public ApiResponse(int _StatusCode , string? _Message = null)
		{
			StatusCode = _StatusCode;
			Message = _Message ?? GetDefaultMessageForStatusCode(StatusCode);
				
		}

		private string? GetDefaultMessageForStatusCode(int statusCode)
		{
			return StatusCode switch 
			{
			
			400 => "Bad Request ",
			401 => "Unuthorize ",
			404 => "Not Found  ",
			_ => null,

			
			};

		}
	}
}
