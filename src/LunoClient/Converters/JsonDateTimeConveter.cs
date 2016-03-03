using System;
using Newtonsoft.Json;

namespace Luno.Converters
{
	internal class JsonDateTimeConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(DateTime));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return DateTime.Parse(reader.Value.ToString());
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(((value as Nullable<DateTime>)?.ToString("yyyy-MM-ddTHH:mm:ss.fff") + "Z"));
		}
	}
}
