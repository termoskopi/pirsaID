using System;
using System.Collections.Generic;
using System.Linq;
using Telkom.Pirsa.VPA.Api.Data.BusinessLogic.Adapter;
using Telkom.Pirsa.VPA.Api.Data.BusinessModel;
using Telkom.Pirsa.VPA.Api.Data.Core;


namespace Telkom.Pirsa.VPA.Api.Data.BusinessLogic
{
  public class AccessTokenRepository : BaseRepository, IRepository
  {
    public AccessTokenRepository(IConnectionManager connection) : base(connection)
    {
      _model = new AccessToken();
      _adapter = new AccessTokenAdapter(_model);
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
        var token = new AccessToken();
        token.Token = id.ToString();
        return SelectBase(token, id);
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
        return SelectBase(new AccessToken());
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
