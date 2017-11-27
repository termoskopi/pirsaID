using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Telkom.Pirsa.VPA.Api.Data.Core;


namespace Telkom.Pirsa.VPA.Api.Data.BusinessModel
{
    [XmlRootAttribute("SystemTask")]
    public class SystemTask : BaseModel, IDataModel
    {
        public SystemTask()
        {
            _table = new Metadata("SystemTask", "SystemTask");
            _storage = new Dictionary<Metadata, object>();

            _columns = new List<Metadata>();
            _columns.Add(new Metadata(ColumnId, ColumnId, true));
            _columns.Add(new Metadata(ColumnActivity, ColumnActivity));
            _columns.Add(new Metadata(ColumnStatus, ColumnStatus));
            _columns.Add(new Metadata(ColumnStatusText, ColumnStatusText));
            _columns.Add(new Metadata(ColumnQueuedDate, ColumnQueuedDate));
            _columns.Add(new Metadata(ColumnStartDate, ColumnStartDate));
            _columns.Add(new Metadata(ColumnFinishedDate, ColumnFinishedDate));

            _columns.ForEach(x => _storage.Add(x, null));
        }
        #region IDataModel Members

        [XmlIgnore]
        public IList<Metadata> ColumnsMetadata
        {
            get { return _columns; }
        }

        [XmlIgnore]
        public Metadata ModelMetadata
        {
            get { return _table; }
        }

        public object GetProperty(Metadata key)
        {
            try
            {
                return GetPropertyBase(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetProperty(Metadata key, object value)
        {
            try
            {
                SetPropertyBase(key, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetProperty(string prop)
        {
            try
            {
                return GetPropertyModelBase(prop);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetProperty(string prop, object value)
        {
            try
            {
                SetPropertyModelBase(prop, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [XmlIgnore]
        public string Xml
        {
            get
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SystemTask));
                TextWriter writer = new StringWriter();
                serializer.Serialize(writer, this);
                writer.Flush();

                return writer.ToString();
            }
        }

        [XmlIgnore]
        public string Json
        {
            get
            {
                JObject json = new JObject()
                {
                    new JProperty(ColumnId, Id),
                    new JProperty(ColumnActivity, Activity),
                    new JProperty(ColumnStatus, Status),
                    new JProperty(ColumnStatusText, StatusText),
                    new JProperty(ColumnQueuedDate, QueuedDate.HasValue ? QueuedDate.Value.ToLocalTime().ToString("dddd, dd MMM yyyy hh:mm:ss tt") : "N/A"),
                    new JProperty(ColumnQueuedDate, StartDate.HasValue ? StartDate.Value.ToLocalTime().ToString("dddd, dd MMM yyyy hh:mm:ss tt") : "N/A"),
                    new JProperty(ColumnQueuedDate, FinishedDate.HasValue ? FinishedDate.Value.ToLocalTime().ToString("dddd, dd MMM yyyy hh:mm:ss tt") : "N/A")
                };

                return json.ToString(Formatting.Indented);
            }
        }

        #endregion

        #region Abstraction Properties
        [XmlElement(IsNullable = false)]
        public int Id
        {
            set
            {
                SetPropertyModelBase(ColumnId, value);
            }
            get
            {
                return Convert.ToInt32(GetPropertyModelBase(ColumnId));
            }
        }

        [XmlElement(IsNullable = false)]
        public string Activity
        {
            set
            {
                SetPropertyModelBase(ColumnActivity, value);
            }
            get
            {
                return Convert.ToString(GetPropertyModelBase(ColumnActivity));
            }
        }

        [XmlElement(IsNullable = false)]
        public int Status
        {
            set
            {
                SetPropertyModelBase(ColumnStatus, value);
            }
            get
            {
                return Convert.ToInt32(GetPropertyModelBase(ColumnStatus));
            }
        }

        [XmlElement(IsNullable = false)]
        public string StatusText
        {
            set
            {
                SetPropertyModelBase(ColumnStatusText, value);
            }
            get
            {
                return Convert.ToString(GetPropertyModelBase(ColumnStatusText));
            }
        }


        [XmlElement(IsNullable = true)]
        public DateTime? QueuedDate
        {
            set
            {
                SetPropertyModelBase(ColumnQueuedDate, value);
            }
            get
            {
                object result = GetPropertyModelBase(ColumnQueuedDate);
                return result != null ? Convert.ToDateTime(result) : new Nullable<DateTime>();
            }
        }

        [XmlElement(IsNullable = true)]
        public DateTime? StartDate
        {
            set
            {
                SetPropertyModelBase(ColumnStartDate, value);
            }
            get
            {
                object result = GetPropertyModelBase(ColumnStartDate);
                return result != null ? Convert.ToDateTime(result) : new Nullable<DateTime>();
            }
        }

        [XmlElement(IsNullable = true)]
        public DateTime? FinishedDate
        {
            set
            {
                SetPropertyModelBase(ColumnFinishedDate, value);
            }
            get
            {
                object result = GetPropertyModelBase(ColumnFinishedDate);
                return result != null ? Convert.ToDateTime(result) : new Nullable<DateTime>();
            }
        }


        #endregion

        #region Column Names
        [XmlIgnore]
        public static string ColumnId
        {
            get { return "Id"; }
        }

        [XmlIgnore]
        public static string ColumnActivity
        {
            get { return "Activity"; }
        }

        [XmlIgnore]
        public static string ColumnStatus
        {
            get { return "Status"; }
        }

        [XmlIgnore]
        public static string ColumnStatusText
        {
            get { return "StatusText"; }
        }

        [XmlIgnore]
        public static string ColumnQueuedDate
        {
            get { return "QueuedDate"; }
        }

        [XmlIgnore]
        public static string ColumnStartDate
        {
            get { return "StartDate"; }
        }

        [XmlIgnore]
        public static string ColumnFinishedDate
        {
            get { return "FinishedDate"; }
        }


        #endregion
    }
}
