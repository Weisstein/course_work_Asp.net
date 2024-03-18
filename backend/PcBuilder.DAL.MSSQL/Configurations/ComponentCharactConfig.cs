using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PcBuilder.DAL.MySQL.Entities;


namespace PcBuilder.DAL.MySQL.Configurations
{
    public class ComponentCharactConfig : IEntityTypeConfiguration<ComponentCharact>
    {
        public void Configure(EntityTypeBuilder<ComponentCharact> builder)
        {
            builder.HasKey(cc => cc.Id);
        }
    }
}
