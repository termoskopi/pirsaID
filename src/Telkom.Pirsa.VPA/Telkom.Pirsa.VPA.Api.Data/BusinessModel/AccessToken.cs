using System;
using System.Collections.Generic;
using System.Linq;
using Telkom.Pirsa.VPA.Api.Data.Core;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Telkom.Pirsa.VPA.Api.Data.BusinessModel
{
  [XmlRootAttribute("AccessToken")]
  public class AccessToken : BaseModel, IDataModel
  {
    public AccessToken()
    {
      _table = new Metadata("AccessToken", "AccessToken");
      _storage = new Dictionary<Metadata, object>();

      _columns = new List<Metadata>();
      _columns.Add(new Metadata(ColumnToken, ColumnToken, true));
      _columns.Add(new Metadata(ColumnUserId, ColumnUserId));
      _columns.Add(new Metadata(ColumnCreatedDate, ColumnCreatedDate));
      _columns.Add(new Metadata(ColumnIsActive, ColumnIsActive));

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
        XmlSerializer serializer = new XmlSerializer(typeof(AccessToken));
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
                    new JProperty(ColumnToken, Token),
                    new JProperty(ColumnUserId, UserId),
                    new JProperty(ColumnCreatedDate, CreatedDate.ToLocalTime().ToString("dddd, dd MMM yyyy hh:mm:ss tt")),
                    new JProperty(ColumnIsActive, ColumnIsActive)
                };

        return json.ToString(Formatting.Indented); 
      }
    }

    #endregion

    #region Abstraction Properties
    [XmlElement(IsNullable = false)]
    public string Token
    {
      set
      {
        SetPropertyModelBase(ColumnToken, value);
      }
      get
      {
        return Convert.ToString(GetPropertyModelBase(ColumnToken));
      }
    }

    [XmlElement(IsNullable = false)]
    public int UserId
    {
      set
      {
        SetPropertyModelBase(ColumnUserId, value);
      }
      get
      {
        return Convert.ToInt32(GetPropertyModelBase(ColumnUserId));
      }
    }

    [XmlElement(IsNullable = false)]
    public DateTime CreatedDate
    {
      set
      {
        SetPropertyModelBase(ColumnCreatedDate, value);
      }
      get
      {
        return Convert.ToDateTime(GetPropertyModelBase(ColumnCreatedDate));
      }
    }

    [XmlElement(IsNullable = false)]
    public int IsActive
    {
      set
      {
        SetPropertyModelBase(ColumnIsActive, value);
      }
      get
      {
        return Convert.ToInt32(GetPropertyModelBase(ColumnIsActive));
      }
    }


    #endregion

    #region Column Names
    [XmlIgnore]
    public static string ColumnToken
    {
      get { return "Token"; }
    }

    [XmlIgnore]
    public static string ColumnUserId
    {
      get { return "UserId"; }
    }

    [XmlIgnore]
    public static string ColumnCreatedDate
    {
      get { return "CreatedDate"; }
    }

    [XmlIgnore]
    public static string ColumnIsActive
    {
      get { return "IsActive"; }
    }
    #endregion
  }
}
