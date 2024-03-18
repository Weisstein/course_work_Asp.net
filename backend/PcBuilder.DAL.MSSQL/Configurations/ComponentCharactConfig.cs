using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PcBuilder.DAL.MySQL.Entities;


namespace PcBuilder.DAL.MySQL.Configurations
{
    public class ComponentCharactConfig : IEntityTypeConfiguration<ComponentCharactEntity>
    {
        public void Configure(EntityTypeBuilder<ComponentCharactEntity> builder)
        {
            builder.HasKey(cc => cc.Id);
        }
    }
}
