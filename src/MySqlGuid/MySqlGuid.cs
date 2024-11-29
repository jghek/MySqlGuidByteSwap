using System;

namespace MySqlGuidByteSwap
{
	/// <summary>
	/// MySqlGuid is a utility class for converting Guids to and from MySql's binary representation. 
	/// MySql uses a different byte order than C# for Guids: 33221100-5544-7766-8899-aabbccddeeff. 
	/// This class provides methods to convert between the two representations. 
	/// </summary>
	public static class MySqlGuid
	{
		/// <summary>
		/// Parses a MySql binary representation of a Guid into a Guid.
		/// </summary>
		/// <param name="b">The byte array that should be transformed.</param>
		/// <returns>A Transformed guid.</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static Guid Parse(byte[] b) =>
			new Guid(swapBytes(b));

		/// <summary>
		/// Transforms a Guid into a MySql binary representation.
		/// </summary>
		/// <param name="guid">The guid that should be transformed.</param>
		/// <returns>A transformed byte array.</returns>
		public static byte[] ToByteArray(Guid guid) =>
			swapBytes(guid.ToByteArray());

		/// <summary>
		/// Swap a Guid bytes to read or write from MySql.
		/// </summary>
		/// <param name="guid">The guid that needs to be changed.</param>
		/// <returns>The changed guid.</returns>
		public static Guid SwapMySqlBytes(this Guid guid) =>
			Parse(swapBytes(ToByteArray(guid)));

		private static byte[] swapBytes(byte[] b)
		{
			if (b.Length != 16)
				throw new ArgumentOutOfRangeException(nameof(b), "Byte array for Guid must be exactly 16 bytes long.");

			return new byte[] { b[3], b[2], b[1], b[0], b[5], b[4], b[7], b[6], b[8], b[9], b[10], b[11], b[12], b[13], b[14], b[15] };
		}
	}
}
