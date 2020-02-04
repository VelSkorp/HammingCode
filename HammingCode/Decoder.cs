using System;
using System.Collections.Generic;
using System.Text;

namespace HammingCode
{
    class Decoder
    {
        private static string Message = "";

        public Decoder(string BinMsgContrBit, string ContrValues)
        {
            Console.WriteLine();
            Console.WriteLine(BinMsgContrBit);

            Coder coder = new Coder();
            coder.CreateMatrixOfX(BinMsgContrBit);
            string Values = coder.GetContrValues;
            Console.WriteLine(Values);

            if (Values == ContrValues)
                Console.WriteLine("ошибок нет");
            else
                BinMsgContrBit=FindFixError(BinMsgContrBit, ContrValues, Values);

            BinToChar(BinMsgContrBit);
        }

        private static string FindFixError(string BinMsgContrBit, string ContrValues, string Values)
        {
            int ErrorBit=0;
            for (int i = 0; i < ContrValues.Length; i++)
            {
                if (ContrValues[i] != Values[i])
                    ErrorBit += (int)Math.Pow(2, i);
            }
            Console.WriteLine("ошибка в: {0} бите",ErrorBit);
            if (BinMsgContrBit[ErrorBit-1]=='1')
                BinMsgContrBit = BinMsgContrBit.Remove(ErrorBit-1, 1).Insert(ErrorBit-1, "0");
            else
                BinMsgContrBit = BinMsgContrBit.Remove(ErrorBit-1, 1).Insert(ErrorBit-1, "1");
            Console.WriteLine(BinMsgContrBit);
            return BinMsgContrBit;
        }

        private static void BinToChar(string BinMsgContrBit)
        {
            int Pow = 0;
            for (int i = 0; i < BinMsgContrBit.Length; i++)
                if (i!=Math.Pow(2,Pow)-1)
                    Message += BinMsgContrBit[i];
                else
                    Pow++;

            List<byte> ByteList = new List<byte>();

            for (int i = 0; i < Message.Length; i+=8)
            {
                ByteList.Add(Convert.ToByte(Message.Substring(i, 8), 2));
            }

            Message = Encoding.UTF8.GetString(ByteList.ToArray());
            Console.WriteLine(Message);
        }

        public string GetMessage
        {
            get { return Message; }
        }
        
    }
}