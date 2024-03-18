using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PcBuilder.DAL.MySQL.Entities;


namespace PcBuilder.DAL.MySQL.Configurations
{
    public class ComponentTypeConfig : IEntityTypeConfiguration<ComponentTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ComponentTypeEntity> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder
                .Property(ct => ct.Name)
                .HasMaxLength();

            builder
                .HasMany(ct => ct.components)
                .WithOne(c => c.type)
                .HasForeignKey(c => c.typeID);

        }
    }
}
