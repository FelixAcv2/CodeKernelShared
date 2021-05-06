using System;
using System.Collections.Generic;
using System.Text;
using Acv2.SharedKernel.Enums;

namespace Acv2.SharedKernel.Interfaces
{
    public interface IStateObject
    {
        ObjectState State { get; }
    }
}
