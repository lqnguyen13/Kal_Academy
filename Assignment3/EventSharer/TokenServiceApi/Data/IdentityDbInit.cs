using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenServiceApi.Models;

namespace TokenServiceApi.Data
{
    public class IdentityDbInit
    {
        private static UserManager<ApplicationUser> _userManager;

        public static async void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            context.Database.Migrate();

            if (context.Users.Any(r => r.UserName == "test@email.com")) return;

            string user = "test@email.com";
            string password = "Password123!";
            await _userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password);
        }
    }
}
