using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PcBuilderApi.Models.Configs
{
    public class ComponentCharactConf : IEntityTypeConfiguration<ComponentCharact>
    {
        public void Configure(EntityTypeBuilder<ComponentCharact> builder)
        {
            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Id)
                .UseMySqlIdentityColumn();

            builder.Property(cc => cc.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.HasIndex(cc => cc.Value);

            builder.Property(cc => cc.Value)
                .HasMaxLength(64)
                .IsRequired();

            builder.HasOne(cc => cc.Component)
                .WithMany(c => c.Characts)
                .HasForeignKey(cc => cc.ComponentId);
        }
    }
}
