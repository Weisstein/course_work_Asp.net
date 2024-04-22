using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PcBuilderApi.Models.Configs
{
    public class ComponentTypeConf : IEntityTypeConfiguration<ComponentType>
    {
        public void Configure(EntityTypeBuilder<ComponentType> builder)
        {
           builder.HasKey(ct => ct.Id);

           builder.HasAlternateKey(ct => ct.Name);

           builder.Property(ct => ct.Name)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
