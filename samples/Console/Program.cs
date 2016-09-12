using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddEntityFrameworkInMemoryDatabase();

            serviceCollection.AddDataProtection()
                .PersistKeysToDatabase(options =>
                {
                    options.UseInMemoryDatabase();
                });

            var services = serviceCollection.BuildServiceProvider();

            var protector = services.GetDataProtector("sample-purpose");
            var protectedData = protector.Protect("Hello world");
            Console.WriteLine(protectedData);

            var context = services.GetService<AspNetCore.DataProtection.EntityFramework.IDataProtectionContext>();
            var key = context.DataProtectionKeys.FirstAsync().Result;
            Console.WriteLine(key.XmlData);
        }
    }
}