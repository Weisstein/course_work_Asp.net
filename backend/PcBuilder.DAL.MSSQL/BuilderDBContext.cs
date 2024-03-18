using Microsoft.EntityFrameworkCore;
using PcBuilder.DAL.MySQL.Configurations;
using PcBuilder.DAL.MySQL.Entities;

namespace PcBuilder.DAL.MySQL
{
    public class BuilderDBContext(DbContextOptions<BuilderDBContext> options)
        : DbContext(options)
    {
        public DbSet<ComponentType> componentTypes { get; set; }

        public DbSet<Component> components { get; set; }

        public DbSet<ComponentCharact> componentCharacts { get; set; }

        public DbSet<Build> builds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComponentTypeConfig());
            modelBuilder.ApplyConfiguration(new ComponentConfig());
            modelBuilder.ApplyConfiguration(new ComponentCharactConfig());
            modelBuilder.ApplyConfiguration(new BuildConfig());

            base.OnModelCreating(modelBuilder);
        }

    }
}
