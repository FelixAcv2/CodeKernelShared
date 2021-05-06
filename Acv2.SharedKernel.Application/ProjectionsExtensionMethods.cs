using System;
using System.Collections.Generic;
using System.Text;
using Acv2.SharedKernel.Crosscutting.Adapter;

namespace Acv2.SharedKernel.Application
{

    public static class ProjectionsExtensionMethods 
    {

        public static TProjection ProjectedAs<TProjection>(this object item) where TProjection : class, new()
        {

            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<TProjection>(item);

        }


        public static List<TProjection> ProjectedAsCollection<TProjection>(this IEnumerable<object> items)
          where TProjection : class, new()
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<List<TProjection>>(items);
        }
    }

}
