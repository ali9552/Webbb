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

    public class websiteconfig : IEntityTypeConfiguration<department>
    {
        public void Configure(EntityTypeBuilder<department> builder)
        {
            
            builder.Property(d=>d.id).UseIdentityColumn(10,10);

        }
    }
}
