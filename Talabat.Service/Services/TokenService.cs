using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Service.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _Configuration;

		public TokenService(IConfiguration configuration)
		{
			_Configuration = configuration;

		}
		public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{

			#region Header of JWT

			#endregion




			#region Payload of JWT

			#region RegisterdClaims
			/// User Defiend Claims
			/// private claims

			var Claims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName,user.DisplayName),
				new Claim(ClaimTypes.Email,user.Email)

			};

			/////////////////// Using Roles AS Claims
			///
			var RolsOfUsers = await userManager.GetRolesAsync(user);

			foreach (var role in RolsOfUsers)
				Claims.Add(new Claim(ClaimTypes.Role, role));

			#endregion

			#region UnRegisterdClaims



			#endregion

			#endregion




			#region Signature of JWT

			#region Key Symmetric

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Key"]));
			#endregion

			var Token = new JwtSecurityToken
				(

				audience: _Configuration["JWT:Audience"],
				issuer: _Configuration["JWT:Issuer"],
				expires: DateTime.Now.AddDays(double.Parse(_Configuration["JWT:ExpireTime"])),
				signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature), /// Header?????
				claims: Claims
				);

			#endregion

			return  new JwtSecurityTokenHandler().WriteToken(Token);

		}
	}
}
