using System;
using System.Text;

namespace HammingCode
{
    class Program
    {
        private static string Message;
        private static string BinaryErrorMessage;
        private static string BinaryMessage;
        private static Coder coder;
        private static Decoder decoder;

        static void Main()
        {
            Console.WriteLine("Введите сообщение");
            Message = Console.ReadLine();
            StringToBinary(Message);
            coder = new Coder(BinaryMessage);

            SendErrorMessage();

            Console.ReadKey();
        }

        private static void StringToBinary(string message)
        {
            byte[] MessageCode = Encoding.UTF8.GetBytes(Message);
            for (int i = 0; i < MessageCode.Length; i++)
                BinaryMessage += Convert.ToString(MessageCode[i], 2).PadLeft(8,'0');
        }

        private static void SendErrorMessage()
        {
            BinaryErrorMessage = coder.GetMessage;
            Console.WriteLine();
            Console.WriteLine("В каком бите ошибка (none - без ошибки)");
            string Error = Console.ReadLine();
            if (Error == "none")
                decoder = new Decoder(BinaryErrorMessage, coder.GetContrValues);
            else
            {
                int index = Convert.ToInt32(Error);
                if (BinaryErrorMessage[index] == '0')
                {
                    BinaryErrorMessage = BinaryErrorMessage.Remove(index - 1, 1).Insert(index - 1, "1");
                    decoder = new Decoder(BinaryErrorMessage, coder.GetContrValues);
                }
                else if (BinaryErrorMessage[index] == '1')
                {
                    BinaryErrorMessage = BinaryErrorMessage.Remove(index - 1, 1).Insert(index - 1, "0");
                    decoder = new Decoder(BinaryErrorMessage, coder.GetContrValues);
                }
                else
                    Console.WriteLine("Неверно введено значение");
            }
        }

    }
}