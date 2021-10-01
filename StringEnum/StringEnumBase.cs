using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StringEnum
{
	public abstract class StringEnumBase<T> : IEquatable<T>
		where T : StringEnumBase<T>
	{
		public enum EnumCase
		{
			Lower,
			Upper,
			AsIs
		}

		public string Value { get; protected set; }

		public override string ToString() => this.Value;

		public static T New([CallerMemberName] string value = null)
		{
			return New(EnumCase.AsIs, value);
		}

		public static T New(EnumCase enumCase, [CallerMemberName] string value = null)
		{
			var objValue = Activator.CreateInstance<T>();
			switch (enumCase)
			{
				case EnumCase.AsIs:
					objValue.Value = value;
					break;

				case EnumCase.Lower:
					objValue.Value = value.ToLower();
					break;

				case EnumCase.Upper:
					objValue.Value = value.ToUpper();
					break;
			}
			return objValue;
		}

		public static List<T> AsList()
		{
			return typeof(T)
				.GetProperties(BindingFlags.Public | BindingFlags.Static)
				.Where(p => p.PropertyType == typeof(T))
				.Select(p => (T)p.GetValue(null))
				.ToList();
		}

		public static T Parse(string value)
		{
			List<T> all = AsList();

			if (!all.Any(a => string.Equals(a.Value, value, StringComparison.OrdinalIgnoreCase)))
				throw new InvalidOperationException($"Value \"{value}\" is not a valid value for the type {typeof(T).Name}");

			return all.Single(a => string.Equals(a.Value, value, StringComparison.OrdinalIgnoreCase));
		}

		public bool Equals(T other)
		{
			if (other == null) return false;
			return string.Equals(this.Value, other?.Value, StringComparison.OrdinalIgnoreCase);
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj is T other) return this.Equals(other);
			return false;
		}

		public override int GetHashCode() => this.Value.ToLower().GetHashCode();

		public static bool operator ==(StringEnumBase<T> a, StringEnumBase<T> b) => a?.Equals(b) ?? false;

		public static bool operator !=(StringEnumBase<T> a, StringEnumBase<T> b) => !(a?.Equals(b) ?? false);

		/// <summary>
		/// For: string cat = MyPet.Cat;
		/// </summary>
		/// <param name="input"></param>
		public static implicit operator string(StringEnumBase<T> input)
		{
			return input.Value;
		}

		/// <summary>
		/// For: MyPet cat = (MyPet)"Cat";
		/// </summary>
		/// <param name="input"></param>
		public static implicit operator StringEnumBase<T>(string input)
		{
			return Parse(input);
		}
	}
}
