using CCC.Data.Monitoring.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCC.Data.Monitoring.Data.Access.EFCore.Mapping
{
    public class QueueGroupMapping : IEntityTypeConfiguration<QueueGroup>
    {
        public void Configure(EntityTypeBuilder<QueueGroup> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name);
            builder.Property(x => x.SLA_Percent);
            builder.Property(x => x.SLA_Time); 
        }
    }
}
