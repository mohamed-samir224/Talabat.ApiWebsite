using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class AddressShipping
    {
		public AddressShipping() { }
		public AddressShipping(string firstName, string lastName, string street, string city, string state, string zipCode, string country, string phoneNumber)
		{
			FName = firstName;
			LName = lastName;
			Street = street;
			City = city;
			State = state;
			Country = country;
			PhoneNumber = phoneNumber;
		}
        public string FName { get; set; }
		public string LName { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public string PhoneNumber { get; set; }

		//public string OrderId { get; set; } // Foreign key to the Order entity
		//public Order Order { get; set; } // Navigation property to the Order entity
										 // Optional: You can add a constructor for easier initialization
	}
}
