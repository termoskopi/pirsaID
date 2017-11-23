using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Research.Web.Nancy.Application.Data.Core
{
  public static class ForEachExtension
  {
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (T item in source)
        action(item);
    }
  }
}
