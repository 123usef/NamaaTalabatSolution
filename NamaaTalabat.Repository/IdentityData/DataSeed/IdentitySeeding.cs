using Microsoft.AspNetCore.Identity;
using NamaaTalabat.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamaaTalabat.Repository.IdentityData.DataSeed
{
    public static class IdentitySeeding
    {

        public static void SeedIdentity(UserManager<ApplicationUser> _userManager)
        {

            if(_userManager.Users.Count() == 0)
            {
                var user = new ApplicationUser()
                {
                    Displayname = "jssngioni" ,
                    Email = "yousif@mohamed.com",
                    PhoneNumber = "01212121212",
                    UserName = "yousif.Mohamed"
                };
                 _userManager.CreateAsync(user , "P@ssWord123");
            }


        }
    }
}
