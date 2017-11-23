using System;
using System.Collections.Generic;
using System.Linq;
using Research.Web.Nancy.Application.Data.Core;


namespace Research.Web.Nancy.Application.Data.BusinessLogic
{
  public class BaseRepository
  {
    protected readonly IConnectionManager _connection;
    protected IDataModel _model;
    protected ISqlAdapter _adapter;

    public BaseRepository(IConnectionManager connection)
    {
      _connection = connection;
    }

    protected bool CreateBase(IDataModel model, bool setPK = false)
    {
      try
      {
        _adapter.BuildQuery(model, AdapterForm.Create, null, setPK);
        string sql = _adapter.Query;

        if (!_connection.IsOpen)
          _connection.Connect();

        return _connection.Execute(sql) > 0;

      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        _connection.Disconnect();
      }
    }

    protected bool UpdateBase(IDataModel model, IList<Metadata> filter)
    {
      try
      {
        if (!_connection.IsOpen)
          _connection.Connect();
        _adapter.BuildQuery(model, AdapterForm.Update, filter);
        string query = _adapter.Query;
        var result = _connection.Execute(query);

        return result > 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        _connection.Disconnect();
      }
    }

    protected bool DeleteBase(IDataModel model, IList<Metadata> filter)
    {
      throw new NotImplementedException();
    }

    protected IDataModel SelectBase(IDataModel model, object id)
    {
      try
      {
        if (!_connection.IsOpen)
          _connection.Connect();

        _adapter.BuildQuery(model, AdapterForm.SelectID);
        string query = _adapter.Query;
        var result = _connection.Query(query);

        _adapter.BuildObject(result, true);

        return _adapter.Model;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        _connection.Disconnect();
      }
    }

    protected IList<IDataModel> SelectBase(IDataModel model)
    {
      try
      {
        if (!_connection.IsOpen)
          _connection.Connect();
        _adapter.BuildQuery(model, AdapterForm.SelectAll);
        string query = _adapter.Query;
        var result = _connection.Query(query);

        _adapter.BuildObject(result, false);

        return _adapter.Collections;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        _connection.Disconnect();
      }
    }

    protected IList<IDataModel> SelectBase(IDataModel model, IList<Metadata> filter)
    {
      try
      {
        if (!_connection.IsOpen)
          _connection.Connect();
        _adapter.BuildQuery(model, AdapterForm.SelectFilter, filter);
        string query = _adapter.Query;
        var result = _connection.Query(query);

        _adapter.BuildObject(result, false);

        return _adapter.Collections;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        _connection.Disconnect();
      }
    }

  }
}
