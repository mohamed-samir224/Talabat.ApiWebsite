namespace Talabat.API.Errors
{
	public class ApiValidationErrorResponse : ApiResponse
	{
		public List<string> Errors { get; set; }

		public ApiValidationErrorResponse(List<string> _Errors)
			:base(400)
		{
			Errors = _Errors;
		}
	}
}
