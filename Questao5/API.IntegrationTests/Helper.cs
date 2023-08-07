using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Http;

namespace API.IntegrationTests
{
    public static class Helper<T> where T : class
    {
        public static StringContent CreateStringContent(object content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        public static Task<T> ReadAsObject(Task<HttpResponseMessage> response)
        {
            var result = response.Result.Content.ReadAsAsync<T>();
            return result;
        }

        public static Task<List<T>> ReadAsList(Task<HttpResponseMessage> response)
        {
            var result = response.Result.Content.ReadAsAsync<List<T>>();
            return result;
        }
    }
}
