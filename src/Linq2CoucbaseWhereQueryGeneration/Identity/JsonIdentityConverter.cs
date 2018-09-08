using System;
using Newtonsoft.Json;

namespace Linq2CoucbaseWhereQueryGeneration.Identity
{
    public class JsonIdentityConverter : JsonConverter
    {
        public override bool CanConvert( Type objectType )
        {
            return typeof( IIdentity ).IsAssignableFrom( objectType );
        }


        public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        {
            return IdConverter.Convert( reader.Value as string, objectType );
        }


        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        {
            writer.WriteValue( ( value as IIdentity ).ToString() );
        }
    }
}
