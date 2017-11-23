using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Research.Web.Nancy.Application.Data.BusinessModel;
using Research.Web.Nancy.Application.Data.Core;
using Research.Web.Nancy.Application.Data.BusinessLogic.Adapter;

namespace Research.Web.Nancy.Application.Data.BusinessLogic
{
  public class UserRepository : BaseRepository, IRepository
  {
    public UserRepository(IConnectionManager connection)
      : base(connection)
    {
      _model = new User();
      _adapter = new UserAdapter(_model);

    }

    #region IRepository Members

    public bool Create(IDataModel model)
    {
      try
      {
        return CreateBase(model);
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
        var user = new User();
        user.Id = Convert.ToInt32(id);
        return SelectBase(user, id);
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
        return SelectBase(new User());
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
