using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Research.Web.Nancy.Application.Data.Core
{
  public interface IRepository
  {
    bool Create(IDataModel model);
    bool Update(IDataModel model, IList<Metadata> filter);
    bool Delete(IDataModel model, IList<Metadata> filter);
    IDataModel Get(object id);
    IList<IDataModel> Get();
    IList<IDataModel> Get(IDataModel model, IList<Metadata> filter);
  }
}
