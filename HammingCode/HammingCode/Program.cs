namespace HammingCode
{
	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("Select mode: encrypt, decode, debug");
			var mode = Console.ReadLine();

			switch (mode)
			{
				case "encrypt":
					Console.Write("Enter a message: ");
					var encryptedMessage = Encrypt(Console.ReadLine());
					Console.WriteLine($"Encrypted message: {encryptedMessage}");
					break;
				case "decode":
					Console.Write("Enter an encrypted message: ");
					var message = Decode(Console.ReadLine());
					Console.WriteLine($"Decrypted message: {message}");
					break;
				case "debug":
					Console.Write("Enter a message: ");
					Debug(Console.ReadLine());
					break;
				default:
					Console.WriteLine("Incorrect operating mode entered");
					break;
			}

			Console.ReadKey();
		}

		private static string Encrypt(string message)
		{
			var binaryMessage = StringToBinaryConverter.StringToBinary(message);
			var coder = new Coder();
			var encryptedMessage = coder.Encrypt(binaryMessage);

			Console.WriteLine($"{message} = {binaryMessage}");
			Console.WriteLine(encryptedMessage);
			for (var j = 0; j < coder.Matrix.Count; j++)
			{
				Console.WriteLine(coder.Matrix[j]);
			}

			return encryptedMessage;
		}

		private static string Decode(string encryptedMessage)
		{
			var decoder = new Decoder();
			var binMessage = decoder.Decode(encryptedMessage);

			Console.WriteLine(encryptedMessage);
			for (var j = 0; j < decoder.Matrix.Count; j++)
			{
				Console.WriteLine(decoder.Matrix[j]);
			}

			if (decoder.Error.HasError)
			{
				Console.WriteLine(decoder.Error.ErrorMessage);
			}
			else
			{
				Console.WriteLine("Was no errors");
			}

			return StringToBinaryConverter.BinaryToString(binMessage);
		}

		private static void Debug(string message)
		{
			var encryptedMessage = Encrypt(message);
			Console.WriteLine($"Encrypted message: {encryptedMessage}");

			Console.WriteLine("\nIn which bit error is (none - no error)?");
			var error = Console.ReadLine();

			if (!error.Equals("none"))
			{
				var index = Convert.ToInt32(error);
				var num = encryptedMessage[index] - '0';
				var newNum = (num ^ 1).ToString();

				encryptedMessage = encryptedMessage.Remove(index - 1, 1).Insert(index - 1, newNum);
			}

			message = Decode(encryptedMessage);
			Console.WriteLine($"Decrypted message: {message}");
		}
	}
}