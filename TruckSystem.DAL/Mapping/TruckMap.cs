using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckSystem.Domain.Vehicles.Model;

namespace TruckSystem.DAL.Mapping
{
    class TruckMap : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.ToTable("TB_Truck");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id).UseSqlServerIdentityColumn();

            builder.HasOne(x => x.Model)
                   .WithMany()
                   .HasForeignKey(x => x.IdModel)
                   .IsRequired();
            
            builder.HasIndex(r => r.Id);
        }
    }
}
