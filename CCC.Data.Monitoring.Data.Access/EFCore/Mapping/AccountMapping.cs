using CCC.Data.Monitoring.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCC.Data.Monitoring.Data.Access.EFCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId);
            builder.Property(x => x.Username); 
        }
    }
}
