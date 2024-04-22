using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PcBuilderApi.Models.Configs
{
    public class ComponentConf : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseMySqlIdentityColumn();

            builder.HasAlternateKey(c => c.Name);

            builder.Property(c => c.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(c => c.Price)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.HasOne(c => c.Type)
                .WithMany(ct => ct.Components)
                .HasForeignKey(c => c.TypeId);

            builder.HasMany(c => c.Builds)
                .WithMany(b => b.Components);
        }
    }
}
