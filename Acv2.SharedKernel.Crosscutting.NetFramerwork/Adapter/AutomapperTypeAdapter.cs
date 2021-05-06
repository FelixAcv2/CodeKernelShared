using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Acv2.SharedKernel.Crosscutting.Adapter;

namespace Acv2.SharedKernel.Crosscutting.NetFramerwork.Adapter
{


    /// <summary>
    /// Automapper type adapter implementation
    /// </summary>
    public class AutomapperTypeAdapter
        : ITypeAdapter
    {
        #region ITypeAdapter Members

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TSource"><see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/></typeparam>
        /// <typeparam name="TTarget"><see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/></typeparam>
        /// <param name="source"><see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            
            return Mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TTarget"><see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/></typeparam>
        /// <param name="source"><see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Crosscutting.Adapter.ITypeAdapter"/></returns>
        public TTarget Adapt<TTarget>(object source) where TTarget : class, new()
        {
            return Mapper.Map<TTarget>(source);
        }

        #endregion
    }


}
