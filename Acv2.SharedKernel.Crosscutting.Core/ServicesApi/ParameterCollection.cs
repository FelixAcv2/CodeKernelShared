using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Acv2.SharedKernel.Crosscutting.Core.ServicesApi
{
  public  class ParameterCollection
    {
        private Dictionary<string, string> _parms = new Dictionary<string, string>();

        public void Add(string key, string val)
        {

            if (_parms.ContainsKey(key))
            {

                throw new InvalidOperationException(string.Format("La clave {0}, ya existe", key));

            }

            _parms.Add(key, val);
        }

        public override string ToString()
        {


            if (!_parms.Any())
                return "";

            var _builder = new StringBuilder("?");

            var _separator = "";

            foreach (var kvp in _parms.Where(kvp => kvp.Value != null))
            {
                _builder.AppendFormat("{0}{1}={2}", _separator, WebUtility.UrlEncode(kvp.Key), WebUtility.UrlDecode(kvp.Value.ToString()));

                _separator = "&";
            }


            return _builder.ToString();
        }

    }
}

