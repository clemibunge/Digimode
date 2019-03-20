using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace DIGIMODE_TX
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        const short FRAME_SIZE = 160;
        static void Main(string[] args)
        {
            TransmitString("Hello World!");
        }


        static void tone(uint freq, uint time)
        {
            Beep(freq, time);
        }



        static void gendtmf()
        {

        }

        static void TransmitString(string input)
        {
            string binstr = StringToBinary(input);
            debug(binstr);
            TransmitBinStr(binstr);
        }

        static void debug(string input)
        {
            Console.WriteLine(input);
        }

        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }


        static void TransmitBinStr(string input)
        {

            //begintone: 300hz for 1000 ms
            //endtone: 350hz for 1000 ms
            //0tone: 700hz for 200 ms
            //1tone: 800hz for 200 ms
            //calibratetone: 200hz for 4000 ms
            //timingtone: 2000hz for 200ms
            tone(200, 4000);
            Thread.Sleep(2000);
            tone(300, 1000);
            Thread.Sleep(300);
            foreach (char c in input)
            {
                Thread.Sleep(10);
                if (c == '0')
                {
                    tone(2000, 200);
                    tone(700, 200);
                    Thread.Sleep(10);
                }
                else if (c == '1')
                {
                    tone(2000, 200);
                    tone(800, 200);
                    Thread.Sleep(10);
                }
            }
            tone(350, 1000);
        }

    }
}
