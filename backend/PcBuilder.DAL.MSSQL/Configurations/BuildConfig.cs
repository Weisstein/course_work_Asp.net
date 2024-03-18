using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.DAL.MySQL.Entities;

namespace PcBuilder.DAL.MySQL.Configurations
{
    public class BuildConfig : IEntityTypeConfiguration<Build>
    {
        public void Configure(EntityTypeBuilder<Build> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .HasMany(b => b.components)
                .WithMany(c => c.builds);
        }
    }
}
