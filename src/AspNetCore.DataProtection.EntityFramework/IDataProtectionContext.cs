using Microsoft.EntityFrameworkCore;

namespace AspNetCore.DataProtection.EntityFramework
{
    public interface IDataProtectionContext
    {
        DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        int SaveChanges();
    }
}