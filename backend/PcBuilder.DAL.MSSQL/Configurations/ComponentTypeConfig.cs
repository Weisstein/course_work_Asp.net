using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PcBuilder.DAL.MySQL.Entities;
using PcBuilder.Core.Models;


namespace PcBuilder.DAL.MySQL.Configurations
{
    public class ComponentTypeConfig : IEntityTypeConfiguration<ComponentTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ComponentTypeEntity> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder.HasAlternateKey(ct => ct.Name);

            builder
                .Property(ct => ct.Name)
                .HasMaxLength(ComponentType.MAS_SIZE_LENGTH)
                .IsRequired();

            builder
                .HasMany(ct => ct.components)
                .WithOne(c => c.Type)
                .HasForeignKey(c => c.TypeID);

        }
    }
}
