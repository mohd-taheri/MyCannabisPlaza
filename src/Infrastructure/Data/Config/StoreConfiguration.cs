using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config
{
    public class StoreConfiguration : IEntityTypeConfiguration<StoreItem>
    {
        public void Configure(EntityTypeBuilder<StoreItem> builder)
        {
            builder.ToTable("Store");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
               .UseHiLo("store_hilo")
               .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.PictureUri)
                .IsRequired(false);                

            builder.Property(s => s.Description)  
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(s => s.License)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(s => s.State)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(s => s.City)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(s => s.Address)
                .IsRequired(false);

            builder.Property(s => s.ZipCode)
                .IsRequired(true);

            builder.Property(s => s.DeliveryZipCode)
                .IsRequired(true);

            builder.Property(s => s.StoreSKU)
                .IsRequired(true);

            //builder.Property(s => s.Location)
            //    .IsRequired(false);      
        }
    }
}
