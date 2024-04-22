using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PcBuilderApi.Models.Configs
{
    public class BuildConf : IEntityTypeConfiguration<Build>
    {
        public void Configure(EntityTypeBuilder<Build> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .UseMySqlIdentityColumn();

            builder.HasAlternateKey(b => b.Name);

            builder.Property(b => b.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasMany(b => b.Components)
                .WithMany(c => c.Builds);
        }
    }
}
