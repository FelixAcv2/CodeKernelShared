#region ensamblado Acv2.SharedKernel.Crosscutting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// C:\Users\FelixAcv2\.nuget\packages\acv2.sharedkernel.tranversales\1.0.0\lib\netcoreapp2.2\Acv2.SharedKernel.Tranversales.dll
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel.Crosscutting.Core.Adapter
{
    /// <summary>
    /// Base contract for adapter factory
    /// </summary>
    public interface ITypeAdapterFactory
    {
        /// <summary>
        /// Create a type adater
        /// </summary>
        /// <returns>The created ITypeAdapter</returns>
        ITypeAdapter Create();
    }
}
