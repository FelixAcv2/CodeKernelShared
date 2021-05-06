using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acv2.SharedKernel.Infraestructure.Enums;

namespace Acv2.SharedKernel.Infraestructure
{
   public class DataBaseConfiguration
    {
        public string ServerName { get; set; }
        public DataBaseTypeConfiguration DataBaseType { get; set; }  //DataBaseTypeConfiguration

        //public DataBaseTypeConfiguration TypeConfiguration { get; set; }
        public string DataBaseName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Port { get; set; }

        public bool Pooling { get; set; } = false;

        public string Scherma { get; set; }

        // public string MyProperty { get; set; }

        public DataBaseConfiguration()
        {

        }

    }
}
