using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PcBuilder.DAL.MySQL.Entities;

namespace PcBuilder.DAL.MySQL.Configurations
{
    public class ComponentTypeConfig : IEntityTypeConfiguration<ComponentType>
    {
        public void Configure(EntityTypeBuilder<ComponentType> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder
                .HasMany(ct => ct.components)
                .WithOne(c => c.type)
                .HasForeignKey(c => c.typeID);

        }
    }
}
