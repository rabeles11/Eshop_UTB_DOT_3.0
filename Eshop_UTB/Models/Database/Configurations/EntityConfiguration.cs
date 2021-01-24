using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models.Database.Configurations
{
    public class EntityConfiguration
    {
        public void Configure(EntityTypeBuilder builder)
        {
            builder.Property(nameof(Entity.DateTimeCreated)).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("GETDATE()");
        }
    }
}
