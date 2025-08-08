using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.API.Extentions
{
	public static class UserManagerExtentions
	{

		public static async Task<AppUser> GetUserWithAddressAsync(this UserManager<AppUser> userManager , ClaimsPrincipal User ) 
		{

			var Email = User.FindFirstValue(ClaimTypes.Email);

			var user = await userManager.Users.Include(U => U.Address)
				.FirstOrDefaultAsync(U =>U.Email == Email);


			return user;
		
		}
	}
}
