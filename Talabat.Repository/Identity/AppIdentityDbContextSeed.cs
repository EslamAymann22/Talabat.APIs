using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {

        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any()) return;
            var User = new AppUser
            {
                DisplayUser = "Eslam Ayman",
                Email = "Eslam.aymann.22a@gmail.com",
                UserName = "Eslam_Aymann22",
                PhoneNumber = "01234567891"
            };
            await userManager.CreateAsync(User, "Pa$$w0rD");
        }

    }
}
