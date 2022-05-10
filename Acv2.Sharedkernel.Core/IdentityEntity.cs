using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel.Core
{
    public abstract class IdentityEntity<Tkey>
    {
        public Tkey Id { get;private set; }



    }
}
