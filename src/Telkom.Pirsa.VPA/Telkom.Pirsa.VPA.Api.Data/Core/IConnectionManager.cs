
namespace Research.Web.Nancy.Application.Data.Core
{
  /// <summary>
  /// Manages connection to a relational database
  /// </summary>
  public interface IConnectionManager
  {
    /// <summary>
    /// Gets the database connection instance of current object instance
    /// </summary>
    System.Data.IDbConnection Connection { get; }

    /// <summary>
    /// Gets database connection string of current database connection object
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// Checks if connection to database is opened
    /// </summary>
    bool IsOpen { get; }

    /// <summary>
    /// Connects to database using current object instance connection string
    /// </summary>
    /// <returns>True if success, false otherwise</returns>
    bool Connect();

    /// <summary>
    /// Disconnects from database specified if a connection opened
    /// </summary>
    void Disconnect();

    /// <summary>
    /// Execute single SQL non query command. 
    /// Throws exception if connection closed.
    /// 
    /// </summary>
    /// <param name="sql">SQL command to execute</param>
    /// <returns>Number rows affected by the operation</returns>
    int Execute(string sql);

    /// <summary>
    /// Execute series of SQL non-query command.
    /// Commands will executed sequentially wrapped in a transaction
    /// </summary>
    /// <param name="sqls">Series of SQL commands to execute</param>
    /// <returns>Number rows affected by operation </returns>
    int Execute(string[] sqls);

    /// <summary>
    /// Execute a single SQL query
    /// </summary>
    /// <param name="sql">SQL query to execute</param>
    /// <returns>Dataset represents</returns>
    System.Data.IDataReader Query(string sql);
  }
}
