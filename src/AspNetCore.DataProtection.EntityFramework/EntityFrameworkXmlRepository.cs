using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace AspNetCore.DataProtection.EntityFramework
{
    public class EntityFrameworkXmlRepository : IXmlRepository
    {
        private IDataProtectionContext _dbContext;
        private string _applicationScope;

        public EntityFrameworkXmlRepository(IDataProtectionContext context, string applicationScope = null)
        {
            _dbContext = context;
            _applicationScope = applicationScope;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var keys = _dbContext.DataProtectionKeys
                .Where(e => e.ApplicationScope == _applicationScope)
                .ToList()
                .Select(e => XDocument.Parse(e.XmlData).Root);

            return new ReadOnlyCollection<XElement>(keys.ToList());
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var key = new DataProtectionKey
            {
                ApplicationScope = _applicationScope,
                FriendlyName = friendlyName,
                XmlData = element.ToString(),
            };

            _dbContext.DataProtectionKeys.Add(key);
            _dbContext.SaveChanges();
        }
    }
}