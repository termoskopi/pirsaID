using System;
using System.Collections.Generic;
using System.Linq;
using Research.Web.Nancy.Application.Data.Core;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Research.Web.Nancy.Application.Data.BusinessModel
{

  [XmlRootAttribute("User")]
  public class User : BaseModel, IDataModel
  {


    public User()
    {
      _table = new Metadata("User", "User");
      _storage = new Dictionary<Metadata, object>();

      _columns = new List<Metadata>();
      _columns.Add(new Metadata(ColumnId, ColumnId, true));
      _columns.Add(new Metadata(ColumnUsername, ColumnUsername));
      _columns.Add(new Metadata(ColumnPassword, ColumnPassword));
      _columns.Add(new Metadata(ColumnLastLogin, ColumnLastLogin));

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
        XmlSerializer serializer = new XmlSerializer(typeof(User));
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
                    new JProperty(ColumnUsername, Username),
                    new JProperty(ColumnPassword, Password),
                    new JProperty(ColumnLastLogin, LastLogin.ToLocalTime().ToString("dddd, dd MMM yyyy hh:mm:ss tt"))
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
    public string Username
    {
      set
      {
        SetPropertyModelBase(ColumnUsername, value);
      }
      get
      {
        return Convert.ToString(GetPropertyModelBase(ColumnUsername));
      }
    }

    [XmlElement(IsNullable = false)]
    public string Password
    {
      set
      {
        SetPropertyModelBase(ColumnPassword, value);
      }
      get
      {
        return Convert.ToString(GetPropertyModelBase(ColumnPassword));
      }
    }

    [XmlElement(IsNullable = false)]
    public DateTime LastLogin
    {
      set
      {
        SetPropertyModelBase(ColumnLastLogin, value);
      }
      get
      {
        return Convert.ToDateTime(GetPropertyModelBase(ColumnLastLogin));
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
    public static string ColumnUsername
    {
      get { return "Username"; }
    }

    [XmlIgnore]
    public static string ColumnPassword
    {
      get { return "Password"; }
    }

    [XmlIgnore]
    public static string ColumnLastLogin
    {
      get { return "LastLogin"; }
    }


    #endregion
  }
}
