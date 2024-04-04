using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.DAL.MySQL.Entities;

namespace PcBuilder.DAL.MySQL.Configurations
{
    public class BuildConfig : IEntityTypeConfiguration<BuildEntity>
    {
        public void Configure(EntityTypeBuilder<BuildEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .Property(b => b.Name)
                .IsRequired();

            builder
                .HasMany(b => b.components)
                .WithMany(c => c.Builds);
        }
    }
}
