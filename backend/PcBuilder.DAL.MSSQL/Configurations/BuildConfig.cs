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
                .HasMany(b => b.components)
                .WithMany(c => c.builds);
        }
    }
}
