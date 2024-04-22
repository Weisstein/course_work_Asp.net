using Microsoft.EntityFrameworkCore;

namespace PcBuilderApi.Data
{
    public class DataContext(DbContextOptions<DbContext> options) : DbContext(options)
    {
    }
}
