using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;

namespace PharmacyData.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Add any extra profile fields later if needed, e.g.:
        // public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
