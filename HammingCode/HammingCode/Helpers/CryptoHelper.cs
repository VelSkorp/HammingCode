using System.Text;

namespace HammingCode
{
	public static class CryptoHelper
	{
		public static List<string> CreateMatrixOfX(int binaryMessageLength, List<int> pows)
		{
			var lineX = new char[binaryMessageLength];
			var matrix = new List<string>();

			foreach (var pow in pows)
			{
				var CounterX = 0;
				Array.Fill(lineX, ' ');
				for (var j = pow - 1; j < binaryMessageLength; j++)
				{
					lineX[j] = 'X';
					CounterX++;

					if (CounterX == pow)
					{
						j += pow;
						CounterX = 0;
					}
				}

				matrix.Add(new string(lineX));
			}

			return matrix;
		}

		public static string СalculateContrBits(string binMsgContrBit, List<string> matrix, List<int> pows)
		{
			var contrValues = new StringBuilder();

			for (var i = 0; i < matrix.Count; i++)
			{
				var lineX = matrix[i];
				var contrBit = 0;

				for (var j = pows[i]; j < binMsgContrBit.Length; j++)
				{
					if (lineX[j] == 'X')
					{
						contrBit ^= binMsgContrBit[j] - '0';
					}
				}

				contrValues.Append(contrBit);
			}

			return contrValues.ToString();
		}
	}
}