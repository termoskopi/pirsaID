using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Data.DataAccess
{
  public static class Db
  {
    private static string cs;
    static Db()
    {
      var setting = new ConnectionSetting();
      cs = string.Format(
        "Data Source={0}; Version={1}; Compress={2}; Password={3}; Pooling=True; Max Pool Size={4}; UTF8Encoding=True; Synchronous=Full;",
        setting.Database, setting.Version, setting.UseCompression, setting.Password, setting.MaxPool);
    }

    public static int Execute(string sql)
    {
      try
      {
        int affected = 0;
        using (var connection = new SQLiteConnection(cs))
        {
          connection.Open();
          using (var cmd = new SQLiteCommand(sql, connection))
          {
            affected = cmd.ExecuteNonQuery();
          }
          connection.Close();
        }
        return affected;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    //public static IDataReader Query(string sql)
    //{
    //  try
    //  {
    //    using (var connection = new SQLiteConnection(cs))
    //    {
    //      IDataReader reader;
    //      connection.Open();
    //      using (var cmd = new SQLiteCommand(sql, connection))
    //      {
    //        reader = cmd.ExecuteReader();
    //      }
    //      connection.Close();
    //      return reader;
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    throw ex;
    //  }
    //}

    public static DataTable Query(string sql)
    {
      try
      {
        using (var connection = new SQLiteConnection(cs))
        {
          IDataReader reader;
          DataSet ds = null;
          connection.Open();
          using (var cmd = new SQLiteCommand(sql, connection))
          {
            reader = cmd.ExecuteReader();
            ds = new DataSet("DataSource");
            ds.Load(reader, LoadOption.Upsert, new[] { "DS" });
          }
          connection.Close();
          return ds.Tables[0];
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
