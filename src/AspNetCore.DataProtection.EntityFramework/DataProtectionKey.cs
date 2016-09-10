
namespace AspNetCore.DataProtection.EntityFramework
{
    public class DataProtectionKey
    {
        public int Id { get; set; }

        public string ApplicationScope { get; set; }

        public string FriendlyName { get; set; }

        public string XmlData { get; set; }
    }
}