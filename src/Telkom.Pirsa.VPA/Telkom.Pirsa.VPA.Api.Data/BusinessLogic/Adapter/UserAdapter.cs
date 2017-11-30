using Telkom.Pirsa.VPA.Api.Data.BusinessModel;
using Telkom.Pirsa.VPA.Api.Data.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Telkom.Pirsa.VPA.Api.Data.BusinessLogic.Adapter
{
  public class UserAdapter : GenericAdapter, ISqlAdapter
  {

    public UserAdapter(IDataModel model)
      : base(model)
    {

    }

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
            IDataModel user = new User();
            _model = FetchRecord(result, user);
          }

        }
        else
        {
          _collection = new List<IDataModel>();
          while (result.Read())
          {
            IDataModel user = new User();
            _collection.Add(FetchRecord(result, user));
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
            BuildSelectQuery(model, model.GetProperty(User.ColumnId));
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

    public System.Collections.Generic.IList<IDataModel> Collections
    {
      get { return _collection; }
    }

    public IDataModel Model
    {
      get { return _model; }
    }

    #region ISqlAdapter Members


    public void BuildObject(System.Data.DataTable result, bool single)
    {
      try
      {
        if (result == null || result.Rows.Count < 0)
        {
          _model = null;
          _collection = new List<IDataModel>();
          return;
        }
        if (single)
        {

          IDataModel user = new User();
          _model = FetchRecord(result.Rows[0], user);
        }
        else
        {
          _collection = new List<IDataModel>();
          for (var rowNum = 0; rowNum < result.Rows.Count; rowNum++ )
          {
            IDataModel user = new User();
            _collection.Add(FetchRecord(result.Rows[rowNum], user));
          }

        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    #endregion
  }
}
