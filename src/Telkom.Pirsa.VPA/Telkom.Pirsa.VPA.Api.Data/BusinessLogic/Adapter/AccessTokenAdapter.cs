using System;
using System.Collections.Generic;
using Telkom.Pirsa.VPA.Api.Data.BusinessModel;
using Telkom.Pirsa.VPA.Api.Data.Core;


namespace Telkom.Pirsa.VPA.Api.Data.BusinessLogic.Adapter
{
  public class AccessTokenAdapter : GenericAdapter, ISqlAdapter
  {
    public AccessTokenAdapter(IDataModel model) : base (model)
    { 
    
    }
    #region ISqlAdapter Members

    public void BuildObject(System.Data.IDataReader result, bool single)
    {
      try
      {
        if (result == null || result.RecordsAffected < 0)
        {
          _model = null;
          _collection = null;
        }
        if (single)
        {
          if (result.Read())
          {
            IDataModel token = new AccessToken();
            _model = FetchRecord(result, token);
          }

        }
        else
        {
          _collection = new List<IDataModel>();
          while (result.Read())
          {
            IDataModel token = new AccessToken();
            _collection.Add(FetchRecord(result, token));
          }

        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void BuildQuery(IDataModel model, AdapterForm queryForm, IList<Metadata> filter = null, bool setPK = false)
    {
      try
      {
        switch (queryForm)
        {
          case AdapterForm.Create:
            BuildCreateQuery(model, setPK);
            break;
          case AdapterForm.SelectAll:
            BuildSelectQuery(model);
            break;
          case AdapterForm.SelectID:
            BuildSelectQuery(model, model.GetProperty(AccessToken.ColumnToken));
            break;
          case AdapterForm.Update:
            BuildUpdateQuery(model, filter);
            break;
          case AdapterForm.SelectFilter:
            BuildSelectQuery(model, filter);
            break;
          default:
            _query = string.Empty;
            break;

        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string Query
    {
      get { return _query; }
    }

    public IList<IDataModel> Collections
    {
      get { return _collection; }
    }

    public IDataModel Model
    {
      get { return _model; }
    }
    #endregion
  }
}
