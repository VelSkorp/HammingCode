using System.Text;

namespace HammingCode
{
	public class Decoder
	{
		public List<string> Matrix { get; private set; } = new List<string>();
		public ErrorDataModel Error { get; private set; } = new ErrorDataModel();

		private readonly List<int> Pows = new List<int>();

		public string Decode(string encryptedMessage)
		{
			CalculatePows(encryptedMessage);
			Matrix = CryptoHelper.CreateMatrixOfX(encryptedMessage.Length, Pows);
			var contrValues = CryptoHelper.СalculateContrBits(encryptedMessage, Matrix, Pows);

			encryptedMessage = FindAndFixError(encryptedMessage, contrValues);

			return RemovePowBits(encryptedMessage);
		}

		private string RemovePowBits(string encryptedMessage)
		{
			var message = new StringBuilder(encryptedMessage);

			for (var i = Pows.Count - 1; i >= 0; i--)
			{
				var index = Pows[i] - 1;
				message.Remove(index, 1);
			}

			return message.ToString();
		}

		private void CalculatePows(string encryptedMessage)
		{
			var i = 0;
			var pow = (int)Math.Pow(2, i);

			while (pow < encryptedMessage.Length)
			{
				Pows.Add(pow);
				pow = (int)Math.Pow(2, ++i);
			}
		}

		private string FindAndFixError(string encryptedMessage, string contrValues)
		{
			var binaryMessage = new StringBuilder(encryptedMessage);
			var errorBit = 0;

			Error.HasError = false;

			for (var i = 0; i < contrValues.Length; i++)
			{
				var index = Pows[i] - 1;
				if (binaryMessage[index] != contrValues[i])
				{
					errorBit += Pows[i];
					Error.HasError = true;
				}
			}

			if (Error.HasError)
			{
				var num = binaryMessage[errorBit - 1] - '0';
				num ^= 1;
				binaryMessage[errorBit - 1] = (char)(num + '0');
				Error.ErrorMessage = $"Error was in: {errorBit} bit";
			}

			return binaryMessage.ToString();
		}
	}
}