using System;
using System.Data;
using System.Data.SQLite;
using Telkom.Pirsa.VPA.Api.Data.Core;

namespace Telkom.Pirsa.VPA.Api.Data.DataAccess
{
  public class SQLiteConnectionManager : IConnectionManager, IDisposable
  {

    private readonly ConnectionSetting _setting;
    private readonly bool _seed;
    private readonly string _connectionString;
    private SQLiteConnection _connection;

    public SQLiteConnectionManager(bool seed = false)
    {
      _seed = seed;
      _setting = new ConnectionSetting();
      _connectionString = BuildConnectionString(_seed);
      _connection = new SQLiteConnection(_connectionString);
    }

    private string BuildConnectionString(bool seed)
    {
      var cs = string.Format(
        "Data Source={0}; Version={1}; Compress={2}; Password={3}; Pooling=True; Max Pool Size={4}; UTF8Encoding=True; Synchronous=Full;", 
        _setting.Database, _setting.Version, _setting.UseCompression, _setting.Password, _setting.MaxPool);
      if (seed)
      {
        cs += "New=True;";
      }

      return cs;
    }

    #region IConnection Members

    public string ConnectionString
    {
      get { return _connectionString; }
    }

    public bool IsOpen
    { 
      get { return _connection != null && _connection.State == ConnectionState.Open; } 
    }

    public IDbConnection Connection
    {
      get { return _connection; }
    }

    public bool Connect()
    {
      try
      {
        if (_connection == null)
          throw new ArgumentException("Connection object must be initialized before it can be used to connect!");
        
        _connection.Open();

        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Disconnect()
    {
      try
      {
        if (IsOpen)
          _connection.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int Execute(string sql)
    {
      try
      {
        if (!IsOpen)
          throw new NullReferenceException("The DB connection currently is not on open state! Please connect before execute command!");

        SQLiteCommand cmd = new SQLiteCommand(sql, _connection);
        var affected = cmd.ExecuteNonQuery();
        if (affected < 0)
          throw new Exception("The last sql execution failed!");

        return affected;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int Execute(string []sqls)
    {
      try
      {
        var affected = 0;
        if (!IsOpen)
          throw new NullReferenceException("The DB connection currently is not on open state! Please connect before execute command!");

        using (var transaction = _connection.BeginTransaction())
        {
          foreach (var sql in sqls)
          {
            SQLiteCommand cmd = new SQLiteCommand(sql, _connection);
            affected += cmd.ExecuteNonQuery();
          }

          transaction.Commit();
          return affected;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public System.Data.IDataReader Query(string sql)
    {
      try
      {
        if (!IsOpen)
          throw new NullReferenceException("The DB connection currently is not on open state! Please connect before execute command!");

        SQLiteCommand cmd = new SQLiteCommand(sql, _connection);

        return cmd.ExecuteReader();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      if (_connection != null)
      {
        if (_connection.State == ConnectionState.Open)
        {
          _connection.Close();
        }
        _connection.Dispose();
      }
    }

    #endregion
  }
}
