using System;
using Checkout.Service.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkout.Service.Converters
{
	public class DiscountConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load(reader);
			switch (jsonObject["Type"].Value<string>())
			{
				case "PercentageDiscount":
					return jsonObject.ToObject<PercentageDiscount>(serializer);
				case "QuantityDiscount":
					return jsonObject.ToObject<QuantityDiscount>(serializer);
			}

			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IDiscount);
		}

		public override bool CanWrite => false;
	}
}
