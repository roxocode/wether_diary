using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBEngine;

namespace TestWetherDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Wether Diary");
            AccessDBEngine engine = new AccessDBEngine();
            engine.Test();

            Console.ReadKey();
        }
    }
}
