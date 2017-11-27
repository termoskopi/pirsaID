using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Telkom.Pirsa.VPA.Api.Data.Core;


namespace Telkom.Pirsa.VPA.Api.Data.BusinessModel
{
    [XmlRootAttribute("Result")]
    public class Result : BaseModel, IDataModel
    {
        public Result()
        {
            _table = new Metadata("Result", "Result");
            _storage = new Dictionary<Metadata, object>();

            _columns = new List<Metadata>();
            _columns.Add(new Metadata(ColumnId, ColumnId, true));
            _columns.Add(new Metadata(ColumnTaskId, ColumnTaskId));
            _columns.Add(new Metadata(ColumnVideoSource, ColumnVideoSource));

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
                XmlSerializer serializer = new XmlSerializer(typeof(Result));
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
                    new JProperty(ColumnTaskId, ColumnTaskId),
                    new JProperty(ColumnVideoSource, VideoSource)
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
        public int TaskId
        {
            set
            {
                SetPropertyModelBase(ColumnTaskId, value);
            }
            get
            {
                return Convert.ToInt32(GetPropertyModelBase(ColumnTaskId));
            }
        }

        [XmlElement(IsNullable = false)]
        public string VideoSource
        {
            set
            {
                SetPropertyModelBase(ColumnVideoSource, value);
            }
            get
            {
                return Convert.ToString(GetPropertyModelBase(ColumnVideoSource));
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
        public static string ColumnTaskId
        {
            get { return "TaskId"; }
        }

        [XmlIgnore]
        public static string ColumnVideoSource
        {
            get { return "VideoSource"; }
        }
        #endregion
    }
}
