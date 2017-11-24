using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Data.Core
{
  public class Metadata
  {
    public Metadata(string db = "db", string model = "Db", bool primary = false)
    {
      Database = db;
      Model = model;
      IsPrimary = primary;
    }
    public string Database { private set; get; }
    public string Model { private set; get; }
    public bool IsPrimary { private set; get; }

    public override bool Equals(object obj)
    {
      return obj != null && (obj as Metadata).Model.Trim() == Model.Trim() && (obj as Metadata).Database.Trim() == Database.Trim();
    }

    //public static bool operator ==(Metadata first, Metadata second)
    //{
    //  if (object.ReferenceEquals(first, null))
    //  {
    //    return object.ReferenceEquals(second, null);
    //  }

    //  return first.Equals(second);
    //}

    //public static bool operator !=(Metadata first, Metadata second)
    //{
    //  if (object.ReferenceEquals(first, null))
    //  {
    //    return !object.ReferenceEquals(second, null);
    //  }

    //  return !first.Equals(second);
    //}
  }
}
