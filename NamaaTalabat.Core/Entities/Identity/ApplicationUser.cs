using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NamaaTalabat.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Displayname { get; set; }
        public string PhoneNumber { get; set; }
        //[NotMapped]
        //public  string Email { get; set; }

        public Address? Address { get; set; }
    }
}
