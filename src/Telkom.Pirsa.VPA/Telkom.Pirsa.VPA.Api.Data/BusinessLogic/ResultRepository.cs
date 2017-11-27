﻿using System;
using System.Collections.Generic;
using System.Linq;
using Telkom.Pirsa.VPA.Api.Data.BusinessLogic.Adapter;
using Telkom.Pirsa.VPA.Api.Data.BusinessModel;
using Telkom.Pirsa.VPA.Api.Data.Core;


namespace Telkom.Pirsa.VPA.Api.Data.BusinessLogic
{
  public class ResultRepository : BaseRepository, IRepository
  {
    public ResultRepository(IConnectionManager connection) : base(connection)
    {
      _model = new Result();
      _adapter = new ResultsAdapter(_model);
    }

    #region IRepository Members

    public bool Create(IDataModel model)
    {
      try
      {
        return CreateBase(model, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool Update(IDataModel model, IList<Metadata> filter)
    {
      try
      {
        return UpdateBase(model, filter);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool Delete(IDataModel model, IList<Metadata> filter)
    {
      try
      {
        return DeleteBase(model, filter);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public IDataModel Get(object id)
    {
      try
      {
        var obj = new Result();
        obj.Id = Convert.ToInt32(id);
        return SelectBase(obj, id);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public IList<IDataModel> Get()
    {
      try
      {
        return SelectBase(new Result());
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public IList<IDataModel> Get(IDataModel model, IList<Metadata> filter)
    {
      try
      {
        return SelectBase(model, filter);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    #endregion
  }
}
