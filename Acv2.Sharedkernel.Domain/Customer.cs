using Acv2.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acv2.Sharedkernel.Domain
{
    public class Customer:EntityGUI
    {
        public string Name { get; set; }


        public Customer()
        {
            this.Id = IdentityGenerator.NewSequentialGuid();
        }
    }
}
