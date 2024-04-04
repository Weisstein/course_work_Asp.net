using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Core.Models;
using PcBuilder.DAL.MySQL.Entities;

namespace PcBuilder.DAL.MySQL.Configurations
{
    public class ComponentConfig : IEntityTypeConfiguration<ComponentEntity>
    {
        public void Configure(EntityTypeBuilder<ComponentEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasAlternateKey(c => c.Title);

            builder
                .Property(c => c.Title)
                .HasMaxLength(Component.MAX_SIZE_LENGTH)
                .IsRequired();

            builder
                .HasMany(c => c.characts)
                .WithOne(cc => cc.component)
                .HasForeignKey(cc => cc.componentId);


            builder
                .HasMany(c => c.builds)
                .WithMany(b => b.components);
        }
    }
}
