using System;
using System.Text;

namespace SerializationExtensions
{
	public static class Serialization
	{

		#region string
		public static byte[] ToBytes(this string value)
		{
			return Encoding.UTF8.GetBytes(value);
		}
		public static string GetString(this byte[] value)
		{
			return Encoding.UTF8.GetString(value);
		}
		#endregion

		#region int
		public static int GetInt32PredefinedSize() => sizeof(int);
		public static byte[] ToBytes(this int value)
		{
			return BitConverter.GetBytes(value);
		}

		public static int GetInt32(this byte[] value)
		{
			return BitConverter.ToInt32(value, 0);
		}
		public static int GetInt(this byte[] value)
		{
			return BitConverter.ToInt32(value, 0);
		}
		#endregion

		#region uint
		public static int GetUInt32PredefinedSize() => sizeof(int);
		public static byte[] ToBytes(this uint value)
		{
			return BitConverter.GetBytes(value);
		}

		public static uint GetUInt32(this byte[] value)
		{
			return BitConverter.ToUInt32(value, 0);
		}
		public static uint GetUInt(this byte[] value)
		{
			return BitConverter.ToUInt32(value, 0);
		}
		#endregion

		#region float
		public static int GetFloatPredefinedSize() => sizeof(float);
		public static int GetSinglePredefinedSize() => sizeof(float);

		public static byte[] ToBytes(this float value)
		{
			return BitConverter.GetBytes(value);
		}

		public static float GetFloat(this byte[] value)
		{
			return BitConverter.ToSingle(value, 0);
		}

		public static float GetSingle(this byte[] value)
		{
			return BitConverter.ToSingle(value, 0);
		}
		#endregion

		#region Boolean
		public static int GetBooleanPredefinedSize() => sizeof(bool);
		public static int GetBoolPredefinedSize() => sizeof(bool);
		public static byte[] ToBytes(this bool value)
		{
			return BitConverter.GetBytes(value);
		}

		public static bool GetBoolean(this byte[] value)
		{
			return BitConverter.ToBoolean(value, 0);
		}

		public static bool GetBool(this byte[] value)
		{
			return BitConverter.ToBoolean(value, 0);
		}
		#endregion
	}
}