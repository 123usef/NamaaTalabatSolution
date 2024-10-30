using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NamaaTalabat.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamaaTalabat.Repository.IdentityData
{
    public class AppIdenittyDbContext :IdentityDbContext<ApplicationUser>
    {
        public AppIdenittyDbContext(DbContextOptions<AppIdenittyDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
