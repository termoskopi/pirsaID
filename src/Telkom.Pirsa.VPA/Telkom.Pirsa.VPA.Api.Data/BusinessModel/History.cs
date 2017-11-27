using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Telkom.Pirsa.VPA.Api.Data.Core;

namespace Telkom.Pirsa.VPA.Api.Data.BusinessModel
{
    [XmlRootAttribute("History")]
    public class History : BaseModel, IDataModel
    {

        public History()
        {
            _table = new Metadata("History", "History");
            _storage = new Dictionary<Metadata, object>();

            _columns = new List<Metadata>();
            _columns.Add(new Metadata(ColumnId, ColumnId, true));
            _columns.Add(new Metadata(ColumnActivity, ColumnActivity));
            _columns.Add(new Metadata(ColumnActionDate, ColumnActionDate));
            _columns.Add(new Metadata(ColumnSource, ColumnSource));

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
                XmlSerializer serializer = new XmlSerializer(typeof(History));
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
                    new JProperty(ColumnActionDate, ActionDate.ToLocalTime().ToString("dddd, dd MMM yyyy hh:mm:ss tt")),
                    new JProperty(ColumnSource, Source)
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
        public DateTime ActionDate
        {
            set
            {
                SetPropertyModelBase(ColumnActionDate, value);
            }
            get
            {
                return Convert.ToDateTime(GetPropertyModelBase(ColumnActionDate));
            }
        }

        [XmlElement(IsNullable = true)]
        public string Source
        {
            set
            {
                SetPropertyModelBase(ColumnSource, value);
            }
            get
            {
                return Convert.ToString(GetPropertyModelBase(ColumnSource));
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
        public static string ColumnActionDate
        {
            get { return "ActionDate"; }
        }

        [XmlIgnore]
        public static string ColumnSource
        {
            get { return "Source"; }
        }
        #endregion
    }
}
