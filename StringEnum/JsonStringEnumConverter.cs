using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StringEnum
{
	public class JsonStringEnumConverter<T> : Newtonsoft.Json.JsonConverter
			where T : StringEnumBase<T>
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => ImplementsGeneric(objectType, typeof(StringEnumBase<>));

		private static bool ImplementsGeneric(Type type, Type generic)
		{
			while (type != null)
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == generic)
					return true;

				type = type.BaseType;
			}

			return false;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JToken item = JToken.Load(reader);
			string value = item.Value<string>();
			return StringEnumBase<T>.Parse(value);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is StringEnumBase<T> v)
				JToken.FromObject(v.Value).WriteTo(writer);
		}
	}
}
