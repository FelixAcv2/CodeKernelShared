using System;
using System.Collections.Generic;
using System.Text;
using Acv2.SharedKernel.Crosscutting.Adapter;

namespace Acv2.SharedKernel.Crosscutting.NetFramerwork.Extensions
{
   public static class ProjectedExtensionMethods
    {
        public static TProjection ProjectedAs<TProjection>(this object item) where TProjection : class, new()
        {

            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<TProjection>(item);

        }


        public static List<TProjection> ProjectedAs<TProjection>(this IEnumerable<object> items)
          where TProjection : class, new()
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<List<TProjection>>(items);
        }

    }
}
