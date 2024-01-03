using System;
using Microsoft.AspNetCore.Identity;

namespace GymApp.Models
{
	public class User : IdentityUser
	{
     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

    }
}

