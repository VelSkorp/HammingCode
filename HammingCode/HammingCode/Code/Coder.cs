using System.Text;

namespace HammingCode
{
	public class Coder
	{
		public List<string> Matrix { get; private set; } = new List<string>();

		private readonly List<int> Pows = new List<int>();

		public string Encrypt(string binMessage)
		{
			var binaryMessage = new StringBuilder(binMessage);

			binaryMessage = CalculatePows(binaryMessage);

			Matrix = CryptoHelper.CreateMatrixOfX(binaryMessage.Length, Pows);
			var contrBits = CryptoHelper.СalculateContrBits(binaryMessage.ToString(), Matrix, Pows);

			return InsertCalculatedContrBits(binaryMessage, contrBits);
		}

		private StringBuilder CalculatePows(StringBuilder binaryMessage)
		{
			var i = 0;
			var pow = (int)Math.Pow(2, i);
			var originalMessageLength = binaryMessage.Length;

			while (pow < originalMessageLength + i + 1)
			{
				binaryMessage.Insert(pow - 1, '0');
				Pows.Add(pow);
				pow = (int)Math.Pow(2, ++i);
			}

			return binaryMessage;
		}

		private string InsertCalculatedContrBits(StringBuilder binaryMessage, string contrBits)
		{
			for (var i = 0; i < contrBits.Length; i++)
			{
				var index = Pows[i] - 1;
				binaryMessage[index] = contrBits[i];
			}

			return binaryMessage.ToString();
		}
	}
}