using Microsoft.EntityFrameworkCore;
using PcBuilderApi.Models;
using PcBuilderApi.Models.Configs;

namespace PcBuilderApi.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<ComponentType> componentTypes { get; set; }
        public DbSet<Component> components { get; set; }
        public DbSet<ComponentCharact> charact { get; set; }
        public DbSet<Build> builds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComponentTypeConf());
            modelBuilder.ApplyConfiguration(new ComponentConf());
            modelBuilder.ApplyConfiguration(new ComponentCharactConf());
            modelBuilder.ApplyConfiguration(new BuildConf());
            base.OnModelCreating(modelBuilder);
        }
    }
}
