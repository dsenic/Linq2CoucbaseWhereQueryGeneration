using System.Collections.Generic;
using Couchbase.Core.Serialization;
using Linq2CoucbaseWhereQueryGeneration.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Linq2CoucbaseWhereQueryGeneration
{
    public class CustomIdEnabledSerializer : DefaultSerializer
    {
        public CustomIdEnabledSerializer()
            : base( CreateSerializerSettings(), CreateSerializerSettings() )
        {
        }

        private static JsonSerializerSettings CreateSerializerSettings()
        {
            var result = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            if ( result.Converters == null )
            {
                result.Converters = new List<JsonConverter>();
            }

            result.Converters.Add( new JsonIdentityConverter() );

            return result;
        }
    }
}
