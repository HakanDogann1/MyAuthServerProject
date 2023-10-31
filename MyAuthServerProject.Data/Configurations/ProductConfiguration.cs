using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAuthServerProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServerProject.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x=>x.Detail).HasMaxLength(350);
            builder.Property(x => x.Price).HasColumnType("decimal(13,2)");
        }
    }
}
