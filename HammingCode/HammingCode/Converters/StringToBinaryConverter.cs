using System.Text;

namespace HammingCode
{
	public static class StringToBinaryConverter
	{
		public static string StringToBinary(string message)
		{
			var binaryMessage = string.Empty;
			var messageCode = Encoding.UTF8.GetBytes(message);

			for (var i = 0; i < messageCode.Length; i++)
			{
				binaryMessage += Convert.ToString(messageCode[i], 2).PadLeft(8, '0');
			}

			return binaryMessage;
		}

		public static string BinaryToString(string message)
		{
			var byteList = new List<byte>();

			for (var i = 0; i < message.Length; i += 8)
			{
				byteList.Add(Convert.ToByte(message.Substring(i, 8), 2));
			}

			return Encoding.UTF8.GetString(byteList.ToArray());
		}
	}
}