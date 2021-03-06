﻿using System;
using System.Collections.Generic;
using System.Linq;
using Telkom.Pirsa.VPA.Api.Data.Core;
using Telkom.Pirsa.VPA.Api.Data.DataAccess;


namespace Telkom.Pirsa.VPA.Api.Data.BusinessLogic
{
  public class BaseRepository
  {
    protected readonly IConnectionManager _connection;
    protected IDataModel _model;
    protected ISqlAdapter _adapter;
    protected readonly object _lock = new object();

    public BaseRepository(IConnectionManager connection)
    {
      _connection = connection;
    }

    protected bool CreateBase(IDataModel model, bool setPK = false)
    {
      //lock (_lock)
      //{
        try
        {
          _adapter.BuildQuery(model, AdapterForm.Create, null, setPK);
          string sql = _adapter.Query;
          return Db.Execute(sql) > 0;
          //if (!_connection.IsOpen)
          //  _connection.Connect();

          //return _connection.Execute(sql) > 0;

        }
        catch (Exception ex)
        {
          throw ex;
        }
        finally
        {
          //_connection.Disconnect();
        }
      //}
    }

    protected bool UpdateBase(IDataModel model, IList<Metadata> filter)
    {
      //lock (_lock)
      //{
        try
        {
          //if (!_connection.IsOpen)
          //  _connection.Connect();
          _adapter.BuildQuery(model, AdapterForm.Update, filter);
          string query = _adapter.Query;
          //var result = _connection.Execute(query);

          return Db.Execute(query) > 0;
        }
        catch (Exception ex)
        {
          throw ex;
        }
        finally
        {
          //_connection.Disconnect();
        }
      //}
    }

    protected bool DeleteBase(IDataModel model, IList<Metadata> filter)
    {
      throw new NotImplementedException();
    }

    protected IDataModel SelectBase(IDataModel model, object id)
    {
      //lock (_lock)
      //{
        try
        {
          //if (!_connection.IsOpen)
          //  _connection.Connect();

          _adapter.BuildQuery(model, AdapterForm.SelectID);
          string query = _adapter.Query;
          //var result = _connection.Query(query);
          var result = Db.Query(query);
          _adapter.BuildObject(result, true);

          return _adapter.Model;
        }
        catch (Exception ex)
        {
          throw ex;
        }
        finally
        {
          //_connection.Disconnect();
        }
      //}
    }

    protected IList<IDataModel> SelectBase(IDataModel model)
    {
      //lock (_lock)
      //{
        try
        {
          //if (!_connection.IsOpen)
          //  _connection.Connect();
          _adapter.BuildQuery(model, AdapterForm.SelectAll);
          string query = _adapter.Query;
          //var result = _connection.Query(query);
          var result = Db.Query(query);
          _adapter.BuildObject(result, false);

          return _adapter.Collections;
        }
        catch (Exception ex)
        {
          throw ex;
        }
        finally
        {
         // _connection.Disconnect();
        }
      //}
    }

    protected IList<IDataModel> SelectBase(IDataModel model, IList<Metadata> filter)
    {
      //lock (_lock)
      //{
        try
        {
          //if (!_connection.IsOpen)
          //  _connection.Connect();
          _adapter.BuildQuery(model, AdapterForm.SelectFilter, filter);
          string query = _adapter.Query;
          //var result = _connection.Query(query);
          var result = Db.Query(query);
          _adapter.BuildObject(result, false);

          return _adapter.Collections;
        }
        catch (Exception ex)
        {
          throw ex;
        }
        finally
        {
          //_connection.Disconnect();
        }
      //}
    }

  }
}
