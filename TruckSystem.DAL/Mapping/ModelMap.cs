using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckSystem.Domain.Vehicles.Model;

namespace TruckSystem.DAL.Mapping
{
    class ModelMap : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("TB_TruckModel");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id).UseSqlServerIdentityColumn();

            builder.HasIndex(r => r.Id);
        }
    }
}
