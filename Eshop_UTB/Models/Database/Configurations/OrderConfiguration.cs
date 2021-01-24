using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models.Database.Configurations
{
    public class OrderConfiguration:EntityConfiguration,IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
    {
            base.Configure(builder);
           /* builder.HasMany(order => order.OrderItems)
                .WithOne(orderItem => orderItem.order)
                .IsRequired()
                .HasForeignKey(orderItem => orderItem.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
           */
    }
}
}
