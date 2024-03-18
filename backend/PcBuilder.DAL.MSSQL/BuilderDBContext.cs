using Microsoft.EntityFrameworkCore;
using PcBuilder.DAL.MySQL.Configurations;
using PcBuilder.DAL.MySQL.Entities;

namespace PcBuilder.DAL.MySQL
{
    public class BuilderDBContext(DbContextOptions<BuilderDBContext> options)
        : DbContext(options)
    {
        public DbSet<ComponentTypeEntity> componentTypes { get; set; }

        public DbSet<ComponentEntity> components { get; set; }

        public DbSet<ComponentCharactEntity> componentCharacts { get; set; }

        public DbSet<BuildEntity> builds { get; set; }

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
