using Telkom.Pirsa.VPA.Api.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Telkom.Pirsa.VPA.Api.Data.BusinessLogic.Adapter
{
    public class GenericAdapter
    {
        protected IDataModel _model;
        protected IList<IDataModel> _collection;
        protected readonly IList<Metadata> _metadata;
        protected readonly Metadata _modelMetadata;
        protected string _query;

        public GenericAdapter(IDataModel model)
        {
            if (model == null)
                throw new ArgumentException("Model blueprint cannot be null!");

            _metadata = model.ColumnsMetadata;
            _modelMetadata = model.ModelMetadata;
        }

        protected IDataModel FetchRecord(IDataReader reader, IDataModel model)
        {
            try
            {
                foreach (var md in _metadata)
                {
                    model.SetProperty(md.Model, reader[md.Database]);
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void ExtractAttributes(IDataModel model, bool skipPrimary, out Dictionary<string, object> attributes)
        {
            try
            {
                if (model == null)
                    throw new ArgumentException("Model data cannot be null!");

                attributes = new Dictionary<string, object>();
                foreach (var attr in model.ColumnsMetadata)
                {
                    if (skipPrimary)
                    {
                        if (attr.IsPrimary)
                            continue;
                    }
                    var value = model.GetProperty(attr);
                    if (value == null)
                        continue;

                    attributes.Add(attr.Database, value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void BuildCreateQuery(IDataModel data, bool setPK = false)
        {
            Dictionary<string, object> attributes;
            ExtractAttributes(data, !setPK, out attributes);
            string columnArgs = string.Empty;
            string valueArgs = string.Empty;

            foreach (var attr in attributes)
            {
                columnArgs += string.Format("[{0}],", attr.Key);
                var format = (attr.Value is string) ? "'{0}'," : 
                    (attr.Value is DateTime) ? "'{0:yyyy-MM-dd HH:mm:ss}'," : "{0},";
                valueArgs += string.Format(format, attr.Value);
            }
            // Removes additional comma
            columnArgs = columnArgs.Remove(columnArgs.Length - 1);
            valueArgs = valueArgs.Remove(valueArgs.Length - 1);
            _query = string.Format("insert into [{0}] ({1}) values ({2});", data.ModelMetadata.Database, columnArgs, valueArgs);
        }

        protected void BuildSelectQuery(IDataModel model)
        {
            _query = string.Format("select * from [{0}]", model.ModelMetadata.Database);
        }

        protected void BuildSelectQuery(IDataModel model, object id)
        {
            var format = (id is string) ? "'{2}'" : "{2}";
            Metadata PK = model.ColumnsMetadata.Where(md => md.IsPrimary).First();
            _query = string.Format("select * from [{0}] where [{1}] = " + format + ";", model.ModelMetadata.Database, PK.Database, model.GetProperty(PK.Model));
        }

        protected void BuildSelectQuery(IDataModel model, IList<Metadata> filterColumn)
        {
            if (filterColumn == null || filterColumn.Count == 0)
            {
                BuildSelectQuery(model);
                return;
            }

            Dictionary<string, object> attributes;
            ExtractAttributes(model, true, out attributes);

            var filterArgs = string.Empty;
            foreach (var md in filterColumn)
            {
                var value = (attributes[md.Database] is string) ? string.Format("'{0}'", attributes[md.Database]) : attributes[md.Database].ToString();
                filterArgs += string.Format("[{0}] = {1} and", md.Database, value);
            }
            // Removes additional comma
            filterArgs = filterArgs.Remove(filterArgs.Length - 4).Trim();

            _query = string.Format("select * from [{0}] where {1};", model.ModelMetadata.Database, filterArgs);
        }

        protected void BuildUpdateQuery(IDataModel data, IList<Metadata> filter)
        {
            if (filter == null || filter.Count == 0)
            {
                throw new Exception("Unsafe operation! Please specify update filter!");
            }

            Dictionary<string, object> attributes;
            ExtractAttributes(data, false, out attributes);
            string columnArgs = string.Empty;
            string filterArgs = string.Empty;
            foreach (var attr in attributes)
            {

                var found = filter.Where(x => x.Database == attr.Key).Any();
                if (found)
                    continue;

                var value = (attr.Value is string) ? string.Format("'{0}'", attr.Value) : attr.Value.ToString();
                columnArgs += string.Format("[{0}] = {1},", attr.Key, value);
            }
            // Removes additional comma
            columnArgs = columnArgs.Remove(columnArgs.Length - 1);
            foreach (var md in filter)
            {
                var value = (attributes[md.Database] is string) ? string.Format("'{0}'", attributes[md.Database]) : attributes[md.Database].ToString();
                filterArgs += string.Format("[{0}] = {1} and", md.Database, value);
            }
            // Removes additional comma
            filterArgs = filterArgs.Remove(filterArgs.Length - 4).Trim();
            _query = string.Format("update [{0}] set {1} where {2};", data.ModelMetadata.Database, columnArgs, filterArgs);
        }


    }
}
