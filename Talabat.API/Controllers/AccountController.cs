using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.Dtos.AccountDTOS;
using Talabat.API.Errors;
using Talabat.API.Extentions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.API.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly UserManager<AppUser> _usermanager;
		private readonly SignInManager<AppUser> _signInManager;
		private ITokenService _tokenservice;
		private readonly IMapper _Mapper;

		public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
		{
			_usermanager = usermanager;
			_signInManager = signInManager;
			_tokenservice = tokenService;

			_Mapper = mapper;
		}


		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _usermanager.FindByEmailAsync(model.Email);
			if (user is null) { return Unauthorized(new ApiResponse(401)); }
			var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!Result.Succeeded) { return Unauthorized(new ApiResponse(401)); }

			return Ok(new UserDto()
			{
				Email = model.Email,
				DisPlayName = user.DisplayName,
				Token = await _tokenservice.CreateTokenAsync(user, _usermanager)

			});
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{

			if (UserExists(model.Email) is { })
			{


				var Nmodel = new AppUser()
				{
					DisplayName = model.DisPlayName,
					Email = model.Email,
					UserName = model.Email.Split("@")[0],
					PhoneNumber = model.PhoneNumber,
					//Address = model.UserAddress // هنا انا بربط العنوان باليوزر الجديد الي هيتسجل
				};

				var Result = await _usermanager.CreateAsync(Nmodel, model.Password);
				if (!Result.Succeeded) { return Unauthorized(new ApiResponse(401)); }
				var token = await _tokenservice.CreateTokenAsync(Nmodel, _usermanager);

				return Ok(new UserDto() { Email = model.Email, DisPlayName = model.DisPlayName, Token = token });
			}

			return BadRequest(new ApiResponse(400, "Email is already in use"));
		}


		[Authorize]
		[HttpGet("CurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{

			var email = User.FindFirstValue(ClaimTypes.Email);

			var user = await _usermanager.FindByEmailAsync(email);

			return Ok(new UserDto()
			{
				DisPlayName = user.DisplayName,
				Email = user.Email ,
				Token = await _tokenservice.CreateTokenAsync(user, _usermanager)
			});

		}

		[Authorize] ////////          معلومه مهمه اي حد عدا من هنا يعني بقا اوسورايز ف دا معناه انه يوزر وعنده توكن وعنده ميزه اول مره اتعرضلها وهي انه بقا(User) ودا معناها اني اقدر استخدمه بال Claims  الي جواه
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto>> GetUserWithAddress()
		{
			//var Email = await _usermanager.FindByEmailAsync(ClaimTypes.Email); // الكومنت الي فوق دا معناه ان السطر دا غلط انا كاتبه   Convention   من عقلي ولكن في حل جديد عليا   

			/*var Email = User.FindFirstValue(ClaimTypes.Email);*/ // دا الجديد عليا وبيرجع حاجه من نوع جديد عليا ف بالتالي لازك احط النوع دا في الكلاس الي بيجيبلي العنوان  

			var user = await _usermanager.GetUserWithAddressAsync(User);

			var Add = _Mapper.Map<AddressDto>(user.Address); // هنا انا بستخدم المابنج عشان احول ال Address الي AddressDto
			return Ok(Add);

		}

		[Authorize]
		[HttpPut("UpdateAddress")]
		public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto NewAddress)
		{

			var Address = _Mapper.Map<AddressDto, Address>(NewAddress);
			var userwithaddress = await _usermanager.GetUserWithAddressAsync(User);

			userwithaddress.Address = Address;

			var result = await _usermanager.UpdateAsync(userwithaddress);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400, "Problem updating user address"));

			return Ok(NewAddress);

		}

		[HttpGet("UserExist")]
		public async Task<ActionResult<bool>> UserExists(string email)
		{
			var Founded = await _usermanager.FindByEmailAsync(email) ;/*!= null*/
			if (Founded is  null)
				return false;
			return true;
		}
	

	
	
	}
}