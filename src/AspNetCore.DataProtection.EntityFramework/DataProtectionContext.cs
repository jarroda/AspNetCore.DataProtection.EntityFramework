
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.DataProtection.EntityFramework
{
    public class DataProtectionContext : DbContext, IDataProtectionContext
    {
        public DataProtectionContext(DbContextOptions<DataProtectionContext> options) : base(options) { }
        
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureDataProtectionContext();
            base.OnModelCreating(builder);
        }
    }
}