using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebIdentityMVC.Context
{
    public class UdemyContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\v11.0;Database=UdemyIdentity;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
