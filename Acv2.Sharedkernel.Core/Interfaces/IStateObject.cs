using System;
using System.Collections.Generic;
using System.Text;
using Acv2.SharedKernel.Core.Enums;

namespace Acv2.SharedKernel.Core.Interfaces
{
    public interface IStateObject
    {
        ObjectState State { get; }
    }
}
