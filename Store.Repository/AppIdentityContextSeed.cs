using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any()) {
                var user = new AppUser
                {
                    DisplayName = "Mahmoud Elbahrawy",
                    Email = "mahmoud@gmail.com",
                    UserName = "mahmoudelbahawy",
                    Address = new Address
                    {
                        FirstName = "Mahmoud",
                        LastName = "Elbahrawy",
                        City = "Elbagour",
                        State = "Elmnofia",
                          Street = "Elsadat",
                          ZipCode = "123456"
                    }
                };
                await userManager.CreateAsync(user,"Password123@");
            }
        }

    }
}
