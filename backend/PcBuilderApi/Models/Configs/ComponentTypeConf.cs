using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PcBuilderApi.Models.Configs
{
    public class ComponentTypeConf : IEntityTypeConfiguration<ComponentType>
    {
        public void Configure(EntityTypeBuilder<ComponentType> builder)
        {
           builder.HasKey(ct => ct.Id);

           builder.Property(ct => ct.Id)
                  .UseMySqlIdentityColumn();

           builder.HasAlternateKey(ct => ct.Name);

           builder.Property(ct => ct.Name)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}
