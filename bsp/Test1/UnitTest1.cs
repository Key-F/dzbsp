using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.IO;

namespace Test1
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void getcoords(string FileName)
        {
            string[] lines = File.ReadAllLines(FileName);
            string[] coord;
            int k = 0; // Счетчик координат
            int count = System.IO.File.ReadAllLines(FileName).Length;
            if (count == 0) Console.WriteLine("Файл пуст");
            foreach (string s in lines)
            {

                coord = s.Split(new char[] { ' ' });
                try
                {
                    Convert.ToDouble(coord[k]);
                }
                catch
                {
                    Console.WriteLine("Некорректные данные");
                }

                //if ((-10000 > Convert.ToDouble(coord[k])) & (Convert.ToDouble(coord[k]) > 10000)) ;     Тут еще не уверен
                //Console.WriteLine("Слишком большие числа");
                k++;
            }
            if (k != 8) Console.WriteLine("Некорректное число координат");
        }
    }
}
