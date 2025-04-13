using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using website.dal.entites;
using website.dal.NewFolder;

namespace website.dal.context
{
    public class websitecontext :IdentityDbContext<appuser>
    {

        public websitecontext(DbContextOptions<websitecontext>options) :base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<appuser>()
     .HasIndex(u => u.Email)
     .IsUnique();
        }
        public DbSet<employee> employees { get; set; }
        public DbSet<department> departments { get; set; }

        


    }
}
