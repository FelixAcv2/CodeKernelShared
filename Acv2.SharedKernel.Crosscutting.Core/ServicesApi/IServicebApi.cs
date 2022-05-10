using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acv2.SharedKernel.Crosscutting.Core.ServicesApi
{
  public  interface IServicebApi
    {
        Task<T> GetAsync<T>(string uri, string authToken = "", string user = null, string userpPassword = null);
        Task<T> PostAsync<T>(string uri, T data, string authToken = "", string user = null, string userpPassword = null);
        Task<T> PutAsync<T>(string uri, T data, string authToken = "", string user = null, string userpPassword = null);
        Task DeleteAsync(string uri, string authToken = "", string user = null, string userpPassword = null);
        Task<R> PostAsync<T, R>(string uri, T data, string authToken = "", string user = null, string userpPassword = null);

    }
}
