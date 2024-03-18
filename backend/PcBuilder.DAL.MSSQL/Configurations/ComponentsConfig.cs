using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.DAL.MySQL.Entities;

namespace PcBuilder.DAL.MySQL.Configurations
{
    public class ComponentConfig : IEntityTypeConfiguration<ComponentEntity>
    {
        public void Configure(EntityTypeBuilder<ComponentEntity> builder)
        {
            builder.HasKey(c => c.Id);

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
