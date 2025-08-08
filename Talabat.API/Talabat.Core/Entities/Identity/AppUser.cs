using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Talabat.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {

        public string DisplayName { get; set; }

		//[RegularExpression(@"^[0-9]{1,3}-[a-zA-z]{5,10}-[a-zA-z]{4,10}-[a-zA-z]{5,10}$"
		//		  , ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
		public Address Address { get; set; }

	}
}
