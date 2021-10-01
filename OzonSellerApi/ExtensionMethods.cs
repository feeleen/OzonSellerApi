using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OzonSellerApi
{
	public static class HttpContentExtension
	{
		public static async Task<string> ReadAsStringUTF8Async(this HttpContent content)
		{
			return await content.ReadAsStringAsync(Encoding.UTF8);
		}

		public static async Task<string> ReadAsStringAsync(this HttpContent content, Encoding encoding)
		{
			using (var reader = new StreamReader((await content.ReadAsStreamAsync()), encoding))
			{
				return reader.ReadToEnd();
			}
		}
	}

	public class EnumValueAttribute : System.Attribute
	{
		private string value;

		public EnumValueAttribute(string value)
		{
			this.value = value;
		}

		public string Value
		{
			get
			{
				return value;
			}
		}
	}

	public static class ExtensionMethods
	{
		public static string EncodeNonAsciiCharacters(this String value)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in value)
			{
				if (c > 127)
				{
					// This character is too big for ASCII
					string encodedValue = "\\u" + ((int)c).ToString("x4");
					sb.Append(encodedValue);
				}
				else
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		public static string DecodeEncodedNonAsciiCharacters(this String value)
		{
			return Regex.Replace(
				value,
				@"\\u(?<Value>[a-zA-Z0-9]{4})",
				m =>
				{
					return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
				});
		}

		/// <summary>
		/// Возвращает строковое значение, заданное в аттрибуте EnumValue. 
		/// Если атрибут не задан - возращается значение ToString()
		/// </summary>
		public static string GetValue(this Enum x)
		{
			var enumType = x.GetType();
			var memberInfos = enumType.GetMember(x.ToString());
			var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
			var valueAttributes =
				  enumValueMemberInfo.GetCustomAttributes(typeof(EnumValueAttribute), false);

			if (valueAttributes.Length > 0)
			{
				var oidRealValue = ((EnumValueAttribute)valueAttributes[0]).Value;
				return oidRealValue;
			}
			else
			{
				return x.ToString();
			}
		}

		public static T ToEnum<T>(this string value, T defaultValue) where T : struct
		{
			if (string.IsNullOrEmpty(value))
			{
				return defaultValue;
			}

			T result;
			return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
		}


		public static Task LoopAsync<T>(this IEnumerable<T> list, Func<T, Task> function)
		{
			return Task.WhenAll(list.Select(function));
		}

		public static async Task<IEnumerable<TOut>> LoopAsyncResult<TIn, TOut>(this IEnumerable<TIn> list, Func<TIn, Task<TOut>> function)
		{
			var loopResult = await Task.WhenAll(list.Select(function));

			return loopResult.ToList().AsEnumerable();
		}
	}
}
