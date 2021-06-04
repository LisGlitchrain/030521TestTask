using SerializationExtensions;
using System;
using System.IO;
using System.Runtime.InteropServices;

public static class SerializationHelper
{

	public static void WriteBytesToStream(byte[] buffer, MemoryStream stream, bool addLengthData = true)
	{
		if (addLengthData)
		{
			// append length of buffer
			int length = Marshal.SizeOf(buffer.Length);
			stream.Write(buffer.Length.ToBytes(), 0, length);
		}

		// append buffer itself
		stream.Write(buffer, 0, buffer.Length);
	}

	public static string GetStringFromBytesIncrementOffset(byte[] input, ref int offset)
	{
		int length = sizeof(int);
		byte[] buffer = new byte[length];
		Buffer.BlockCopy(input, offset, buffer, 0, length);
		offset += length;

		length = buffer.GetInt();

		buffer = new byte[length];
		Buffer.BlockCopy(input, offset, buffer, 0, length);
		offset += length;

		return buffer.GetString();
	}
}
