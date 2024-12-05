using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {

        public string DisplayUser { get; set; }
        public Address Address { get; set; }

        UserManager<AppUser> UserManager { get; set; }
    }
}
