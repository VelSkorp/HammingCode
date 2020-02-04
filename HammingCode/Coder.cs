using System;

namespace HammingCode
{
    class Coder
    {
        private static string BinMsgContrBit;
        private static string ContrValues;
        private static int[] ArrayOfPow = new int[20];

        public Coder()//for decoder
        {
            ContrValues = string.Empty;
        }

        public Coder(string BinaryMessage)//for get binary code from message
        {
            for (int i = 0; i < 20; i++)
                if (BinaryMessage.Length >= (int)Math.Pow(2, i))
                    ArrayOfPow[i] = (int)Math.Pow(2, i);

            int NumPow=0;
            for (int i = 0; i < ArrayOfPow.Length; i++)
                if (ArrayOfPow[i] != 0)
                    NumPow++;

            int LenghtMsgWithContrBit = BinaryMessage.Length + NumPow;
            CreateBinMsgContrBit(BinaryMessage, LenghtMsgWithContrBit);

            Console.WriteLine(BinMsgContrBit);

            CreateMatrixOfX(BinMsgContrBit);

            Console.WriteLine(ContrValues);
        }

        private void CreateBinMsgContrBit(string BinaryMessage, int LenghtMsg)
        {
            int NumBit = 0;
            int NumContrBit = 0;
            for (int i = 1; i <= LenghtMsg; i++)
            {
                if (i == ArrayOfPow[NumContrBit])
                {
                    BinMsgContrBit += "0";
                    if (NumContrBit < ArrayOfPow.Length - 1)
                    {
                        if (ArrayOfPow[NumContrBit] != 0)
                        {
                            NumContrBit++;
                        }
                    }
                }
                else
                {
                    BinMsgContrBit += BinaryMessage[NumBit];
                    if (NumBit < BinaryMessage.Length - 1)
                    {
                        NumBit++;
                    }
                }
            }
        }

        public void CreateMatrixOfX(string BinMsgContrBit)
        {
            char[] LineX = new char[BinMsgContrBit.Length];
            int Pow;

            for (int i = 0; i < 20; i++)
            {
                Pow = (int)Math.Pow(2, i);
                if (BinMsgContrBit.Length > Pow)
                {
                    bool IsPow = false;
                    int CounterX = 0;
                    for (int j = Pow - 1; j < BinMsgContrBit.Length; j++)
                    {
                        if (!IsPow)
                        {
                            LineX[j] = 'X';
                            CounterX++;
                        }
                        else if (IsPow)
                        {
                            LineX[j] = ' ';
                            CounterX--;
                        }
                        else
                        {
                            CounterX = 0;
                        }
                        if (CounterX == Pow)
                            IsPow = true;
                        if (CounterX == 0)
                            IsPow = false;
                    }

                    CreateArrayOfContrBit(BinMsgContrBit, LineX);

                    for (int j = 0; j < LineX.Length; j++)
                    {
                        Console.Write(LineX[j]);
                        LineX[j] = ' ';
                    }
                    Console.WriteLine(); 
                }
            }
        }

        private void CreateArrayOfContrBit(string BinMsgContrBit,char[] LineX)
        {
            int CounterArrayOfContrBits = 0;

            for (int i = 0; i < BinMsgContrBit.Length; i++)
            {
                if (LineX[i] == 'X')
                {
                    if (BinMsgContrBit[i] == '1')
                    {
                        CounterArrayOfContrBits++;
                    }
                }
            }
            if (CounterArrayOfContrBits % 2 == 0)
                ContrValues+= "0";
            else
                ContrValues+= "1";
        }

        public string GetMessage
        {
            get { return BinMsgContrBit; }
        }

        public string GetContrValues
        {
            get { return ContrValues; }
        }
    }
}