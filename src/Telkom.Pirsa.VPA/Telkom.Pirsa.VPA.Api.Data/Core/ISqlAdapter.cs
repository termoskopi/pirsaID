using System.Collections.Generic;
using System.Data;

namespace Research.Web.Nancy.Application.Data.Core
{
  /// <summary>
  /// Adapt Model structure to SQL queries and otherwise 
  /// </summary>
  public interface ISqlAdapter
  {
    /// <summary>
    /// Builds  IDataModel instance or IDataModel collection from IDataReader object instance results
    /// </summary>
    /// <param name="result">IDataReader object to convert</param>
    /// <param name="single">Boolean marker to indicate the IDataReader instance will only contain a single record</param>
    void BuildObject(IDataReader result, bool single);

    /// <summary>
    /// Builds sql string from IDataModel instances 
    /// </summary>
    /// <param name="model">IDataModel instance to build</param>
    /// <param name="queryForm">Enumeration of query form type</param>
    /// <param name="filter">List of filter column (OPTIONAL)</param>
    void BuildQuery(IDataModel model, AdapterForm queryForm, IList<Metadata> filter = null, bool setPK = false);

    /// <summary>
    /// Gets query string result
    /// </summary>
    string Query { get; }

    /// <summary>
    /// Gets IDataModel collection result
    /// </summary>
    IList<IDataModel> Collections { get; }

    /// <summary>
    /// Gets IDataModel object result
    /// </summary>
    IDataModel Model { get; }
  }
}
