using System;
using System.Text;

namespace HammingCode
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("вуберите режим: code(кодировать), decode(декодировать), debug(отладка)");
            string mode = Console.ReadLine();
            if (mode == "code")
            {
                Console.WriteLine("Введите сообщение");
                string Message = Console.ReadLine();
                string BinaryMessage = StringToBinary(Message);
                Coder coder = new Coder(BinaryMessage);
                Console.WriteLine("закодированное сообщение: {0}",coder.GetMessage);
                Console.WriteLine("контрольные значения: {0}",coder.GetContrValues);
            }
            else if (mode == "decode")
            {
                Console.Write("Введите закодированное сообщение: ");
                string BinMsgContrBit = Console.ReadLine();
                Console.Write("Введите контрольные значения: ");
                string ContrValues = Console.ReadLine();
                Decoder decoder = new Decoder(BinMsgContrBit, ContrValues);
            }
            else if (mode == "debug")
            {
                Console.WriteLine("Введите сообщение");
                string Message = Console.ReadLine();
                string BinaryMessage = StringToBinary(Message);
                Coder coder = new Coder(BinaryMessage);
                SendErrorMessage(coder);
            }
            else
                Console.WriteLine("Неверно введен режим работы");

            Console.ReadKey();
        }

        private static string StringToBinary(string message)
        {
            string BinaryMessage="";
            byte[] MessageCode = Encoding.UTF8.GetBytes(message);
            for (int i = 0; i < MessageCode.Length; i++)
                 BinaryMessage += Convert.ToString(MessageCode[i], 2).PadLeft(8,'0');
            return BinaryMessage;
        }

        private static void SendErrorMessage(Coder coder)
        {
            Decoder decoder;
            string BinaryErrorMessage = coder.GetMessage;
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