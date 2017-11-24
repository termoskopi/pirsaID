using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Data.Core
{
  public interface IDataModel
  {
    IList<Metadata> ColumnsMetadata { get; }
    Metadata ModelMetadata { get; }
    object GetProperty(Metadata key);
    object GetProperty(string prop);
    void SetProperty(Metadata key, object value);
    void SetProperty(string prop, object value);
    string Xml { get; }
    string Json { get; }

  }
}
