using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicians_practice_log_api
{
    public static class Helper
    {
        public static string ParseToJsonString<T>(T collection)
        {
            return JsonConvert.SerializeObject(collection, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }
    }
}
