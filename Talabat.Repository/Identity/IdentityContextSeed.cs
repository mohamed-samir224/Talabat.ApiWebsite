using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class IdentityContextSeed 
    {
		public async static Task SeedUser(UserManager<AppUser> userManager)
		{

			if (!userManager.Users.Any())
			{
				var user = new AppUser()
				{
					DisplayName = "Mohamed Samir",
					Email = "mhmdnagi7778@gmail.com",
					PhoneNumber = "01032821244",
					UserName = "MohamedSamirNagi",
					Address = new Address()
					{
						FirstName = "Mohamed",
						LastName = "Samir",
						Street = "Elmadares Street",
						City = "Nasr City",
						Country = "Egypt",

					}

				};

				await userManager.CreateAsync(user, "@Pa$$word");
			}
			


		}

		


	}
}
