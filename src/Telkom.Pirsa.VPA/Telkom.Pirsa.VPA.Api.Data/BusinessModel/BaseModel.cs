using Telkom.Pirsa.VPA.Api.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Telkom.Pirsa.VPA.Api.Data.BusinessModel
{
    public class BaseModel
    {
        protected Metadata _table;
        protected IList<Metadata> _columns;
        protected Dictionary<Metadata, object> _storage;

        protected Metadata GetMetadataByProperty(string property)
        {
            if (string.IsNullOrEmpty(property))
                throw new ArgumentException("The specified property not valid!");

            if (_columns == null)
                throw new Exception("The metadata definition not available!");

            var metadataDefinition = _columns.Where(md => md.Model.Trim() == property.Trim());

            if (!metadataDefinition.Any() || !_storage.ContainsKey(metadataDefinition.First()))
                throw new ArgumentException("The specified property not present in model definition!");

            return metadataDefinition.First();
        }

        protected Metadata GetMetadataByColumn(string column)
        {
            if (string.IsNullOrEmpty(column))
                throw new ArgumentException("The specified column not valid!");

            if (_columns == null)
                throw new Exception("The metadata definition not available!");

            var metadataDefinition = _columns.Where(md => md.Database.Trim() == column.Trim());

            if (!metadataDefinition.Any() || !_storage.ContainsKey(metadataDefinition.First()))
                throw new ArgumentException("The specified column not present in table definition!");

            return metadataDefinition.First();
        
        }

        protected void SetPropertyModelBase(string propName, object value)
        {
            try
            {
                var metadata = GetMetadataByProperty(propName);
                _storage[metadata] = value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SetPropertyTableBase(string columnName, object value)
        {
            try
            {
                var metadata = GetMetadataByColumn(columnName);
                _storage[metadata] = value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SetPropertyBase(Metadata metadata, object value)
        {
            try
            {
                if (metadata == null)
                    throw new ArgumentException("The specified metadata is not valid!");

                //if (!_storage.ContainsKey(metadata))
                //    throw new ArgumentException("The specified metadata not present in table and model configuration!");

                _storage[metadata] = value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected object GetPropertyModelBase(string propName)
        {
            try
            {
                var metadata = GetMetadataByProperty(propName);
                return _storage[metadata];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected object GetPropertyTableBase(string columnName)
        {
            try
            {
                var metadata = GetMetadataByColumn(columnName);
                return _storage[metadata];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected object GetPropertyBase(Metadata metadata)
        {
            try
            {
                if (metadata == null)
                    throw new ArgumentException("The specified metadata is not valid!");

                if (!_storage.ContainsKey(metadata))
                    throw new ArgumentException("The specified metadata not present in table and model configuration!");

                return _storage[metadata];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
