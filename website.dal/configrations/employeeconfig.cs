using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using website.dal.entites;

namespace website.dal.configrations
{
    public class employeeconfig : IEntityTypeConfiguration<employee>
    {
        public void Configure(EntityTypeBuilder<employee> builder)
        {
            builder.Property(e => e.id).UseIdentityColumn(10, 10);
            builder.HasOne(e=>e.department).WithMany(e => e.employees).HasForeignKey(e => e.departmentid).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
