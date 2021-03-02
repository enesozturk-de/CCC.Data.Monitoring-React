using CCC.Data.Monitoring.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCC.Data.Monitoring.Data.Access.EFCore.Mapping
{
    public class MonitorDataMapping : IEntityTypeConfiguration<MonitorData>
    {
        public void Configure(EntityTypeBuilder<MonitorData> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.AfterCallWorkTime);
            builder.Property(x => x.Handled);
            builder.Property(x => x.HandledWithinSL);
            builder.Property(x => x.Offered);
            builder.Property(x => x.TalkTime);
            builder.Property(x => x.QueueGroupId);
        }
    }
}
