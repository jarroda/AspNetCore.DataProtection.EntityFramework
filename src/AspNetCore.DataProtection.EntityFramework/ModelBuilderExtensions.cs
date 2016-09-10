
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.DataProtection.EntityFramework
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureDataProtectionContext(this ModelBuilder builder)
        {
            builder.Entity<DataProtectionKey>(key =>
            {
                //.ToTable("DataProtectionKeys")
                key.HasKey(e => e.Id);
                key.Property(e => e.XmlData).IsRequired();
            });
        }
    }
}